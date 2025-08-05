using Application.Features.MediatR.PaymentInstallments.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.MediatR.PaymentInstallments.Handlers.Write
{
    public class UpdatePaymentInstallmentCommandHandler : IRequestHandler<UpdatePaymentInstallmentCommand, int>
    {
        private readonly IRepository<PaymentInstallment> _repository;
        private readonly IRepository<Rental> _rentalRepository;
        public UpdatePaymentInstallmentCommandHandler(IRepository<PaymentInstallment> repository, IRepository<Rental> rentalRepository)
        {
            _repository = repository;
            _rentalRepository = rentalRepository;
        }

        public async Task<int> Handle(UpdatePaymentInstallmentCommand request, CancellationToken cancellationToken)
        {
            var installment = await _repository.ListByFilterAsync(filter:x=>x.RentalId==request.RentalId && x.DueDate>DateTime.Now && x.IsPaid==false,orderByDescending:x=>x.DueDate);
            var rental = await _rentalRepository.GetByIdAsync(request.RentalId);

            decimal eklenecekTutar=0;
            foreach (var i in installment)
            {
                var yeniDeğer = i.Amount+((i.Amount * request.Percent) / 100);
                var aradakiFark = yeniDeğer - i.Amount;
                i.Amount = yeniDeğer;
                await _repository.UpdateAsync(i);
                eklenecekTutar = eklenecekTutar + aradakiFark;
            }
            rental.TotalAmount += eklenecekTutar;
            await _rentalRepository.UpdateAsync(rental);


            return 0;
        }
    }
}
