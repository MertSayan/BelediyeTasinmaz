using Application.Constans;
using Application.Features.MediatR.Rentals.Commands;
using Application.Interfaces.PaymentInstallmentInterface;
using Application.Interfaces.RentalInterface;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Write
{
    public class ExtendRentalCommandHandler : IRequestHandler<ExtendRentalCommand, Unit>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IPaymentInstallmentRepository _installmentRepository;

        public ExtendRentalCommandHandler(
            IRentalRepository rentalRepository,
            IPaymentInstallmentRepository installmentRepository)
        {
            _rentalRepository = rentalRepository;
            _installmentRepository = installmentRepository;
        }

        public async Task<Unit> Handle(ExtendRentalCommand request, CancellationToken ct)
        {
            // Kiralama + taksitleri birlikte çek (repo bu include'ları sağlıyor)
            var rental = await _rentalRepository.GetRentalWithDetailsAsync(request.RentalId)
                         ?? throw new Exception(Messages<Rental>.EntityNotFound);

            if (!rental.IsActive)
                throw new Exception("Pasif kiralama uzatılamaz.");

            if (request.NewEndDate <= rental.EndDate)
                throw new Exception("Yeni bitiş tarihi mevcut bitişten büyük olmalı.");

            if (request.AdditionalAmount < 0)
                throw new Exception("Ek tutar negatif olamaz.");

            // Mevcut tüm taksitleri al (IRepository.ListByFilterAsync mevcut)
            var allInstallments = await _installmentRepository.ListByFilterAsync(
                i => i.RentalId == rental.RentalId,
                orderByDescending: x => x.DueDate // en yenisi sonda olsun diye az sonra ters çevireceğiz
            );

            var paid = allInstallments.Where(i => i.IsPaid).ToList();
            var unpaid = allInstallments.Where(i => !i.IsPaid).ToList();

            decimal outstanding = unpaid.Sum(i => i.Amount);
            decimal replanTotal = outstanding + request.AdditionalAmount;
            if (replanTotal <= 0)
                throw new Exception("Yeniden planlanacak tutar bulunamadı.");

            // Yeni planın başlangıç noktası: son ödenen taksidin vadesi (yoksa kiralamanın başlangıcı)
            DateTime startFromExclusive = paid.Any()
                ? paid.Max(i => i.DueDate)
                : rental.StartDate;

            var frequency = request.PaymentFrequencyOverride ?? rental.PaymentFrequency;

            // --- Eski ödenmemişleri sil ---
            // IRepository.DeleteAsync sadece Id ile çalıştığı için tek tek sileceğiz
            foreach (var inst in unpaid)
                await _installmentRepository.DeleteAsync(inst.PaymentInstallmentId);

            // --- Yeni planı oluştur ---
            var newPlan = BuildSchedule(
                rentalId: rental.RentalId,
                startFromExclusive: startFromExclusive,
                newEndDate: request.NewEndDate,
                frequency: frequency,
                totalAmount: replanTotal
            );

            // Ekle (AddRange yok; tek tek ekle)
            foreach (var inst in newPlan)
                await _installmentRepository.AddAsync(inst);

            // Kiralama alanlarını güncelle
            rental.EndDate = request.NewEndDate;
            rental.TotalAmount += request.AdditionalAmount; // Politikanıza göre ayarlayın

            await _rentalRepository.UpdateAsync(rental);

            return Unit.Value;
        }

        private static List<PaymentInstallment> BuildSchedule(
            int rentalId,
            DateTime startFromExclusive,
            DateTime newEndDate,
            PaymentFrequency frequency,
            decimal totalAmount)
        {
            if (newEndDate <= startFromExclusive)
                throw new ArgumentException("Yeni bitiş tarihi başlangıçtan küçük/eşit olamaz.");

            // Dönem son tarihlerini üret
            var dates = new List<DateTime>();
            DateTime cursor = startFromExclusive;
            while (true)
            {
                cursor = frequency == PaymentFrequency.Monthly ? cursor.AddMonths(1) : cursor.AddDays(7);
                if (cursor > newEndDate) cursor = newEndDate;
                dates.Add(cursor);
                if (cursor == newEndDate) break;
            }

            int periods = dates.Count;
            if (periods <= 0) throw new InvalidOperationException("Plan oluşturulamadı.");

            // Eşit dağıtım + son takside bakiye (kurus farkı)
            decimal per = Math.Round(totalAmount / periods, 2);
            decimal last = totalAmount - per * (periods - 1);

            var list = new List<PaymentInstallment>(periods);
            for (int i = 0; i < periods; i++)
            {
                list.Add(new PaymentInstallment
                {
                    RentalId = rentalId,
                    DueDate = dates[i],
                    Amount = (i == periods - 1) ? last : per,
                    IsPaid = false
                });
            }
            return list;
        }
    }
}
