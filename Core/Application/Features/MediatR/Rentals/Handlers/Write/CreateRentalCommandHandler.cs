using Application.Constans;
using Application.Features.MediatR.Rentals.Commands;
using Application.Interfaces.PaymentInstallmentInterface;
using Application.Interfaces.PropertyInterface;
using Application.Interfaces.RentalInterface;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Write
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Unit>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IPaymentInstallmentRepository _installmentRepository;
        private readonly IMapper _mapper;
        private readonly IRentalReportService _rentalReportService;
        public CreateRentalCommandHandler(IPropertyRepository propertyRepository, IRentalRepository rentalRepository, IMapper mapper, IPaymentInstallmentRepository installmentRepository, IRentalReportService rentalReportService)
        {
            _propertyRepository = propertyRepository;
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _installmentRepository = installmentRepository;
            _rentalReportService = rentalReportService;
        }

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
            if (property == null || property.Status == PropertyStatus.Rented)
                throw new Exception(Messages<Property>.PropertyNotAvalible);

            var rental = _mapper.Map<Rental>(request);

            rental.CreatedAt = DateTime.Now;
            rental.IsActive = true;
            await _rentalRepository.AddAsync(rental);

            property.Status = PropertyStatus.Rented;
            await _propertyRepository.UpdateAsync(property);

            var installments = new List<PaymentInstallment>();
            DateTime current = request.StartDate;
            int installmentCount = 0;

            // Önce kaç taksit olacağını hesapla
            DateTime temp = request.StartDate;
            while (temp < request.EndDate)
            {
                temp = request.PaymentFrequency == PaymentFrequency.Monthly
                    ? temp.AddMonths(1)
                    : temp.AddDays(7);

                installmentCount++;
            }

            // Her taksit için eşit tutar
            decimal installmentAmount = Math.Round(request.TotalAmount / installmentCount, 2); // Virgülden sonra 2 basamak

            // Taksitleri oluştur
            while (current < request.EndDate)
            {
                var dueDate = request.PaymentFrequency == PaymentFrequency.Monthly
                    ? current.AddMonths(1)
                    : current.AddDays(7);

                if (dueDate > request.EndDate)
                    dueDate = request.EndDate;

                installments.Add(new PaymentInstallment
                {
                    RentalId = rental.RentalId,
                    DueDate = dueDate,
                    Amount = installmentAmount,
                    IsPaid = false
                });

                current = dueDate;
            }

            // Veritabanına taksitleri kaydet
            foreach (var installment in installments)
                await _installmentRepository.AddAsync(installment);

            await _rentalReportService.GenerateReportAsync(rental.RentalId);

            return Unit.Value;
        }
    }
}
