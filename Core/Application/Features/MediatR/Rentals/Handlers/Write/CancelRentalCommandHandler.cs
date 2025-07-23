using Application.Constans;
using Application.Features.MediatR.Rentals.Commands;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Write
{
    public class CancelRentalCommandHandler : IRequestHandler<CancelRentalCommand, Unit>
    {
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IRepository<PaymentInstallment> _installmentRepository;

        public CancelRentalCommandHandler(IRepository<Rental> rentalRepository, IRepository<Property> propertyRepository, IRepository<PaymentInstallment> installmentRepository)
        {
            _rentalRepository = rentalRepository;
            _propertyRepository = propertyRepository;
            _installmentRepository = installmentRepository;
        }

        public async Task<Unit> Handle(CancelRentalCommand request, CancellationToken cancellationToken)
        {
            var rental=await _rentalRepository.GetByFilterAsync(x=>x.RentalId==request.RentalId,x=>x.Property, x=>x.PaymentInstallments);
            if (rental == null) throw new Exception(Messages<Rental>.EntityNotFound);

            rental.IsActive = false;

            rental.Property.Status = PropertyStatus.Available;
            rental.CancelByUserId = request.CancelByUserId;
            rental.CancelAt=DateTime.Now;

            await _propertyRepository.UpdateAsync(rental.Property);
            await _rentalRepository.UpdateAsync(rental);

            var installments = rental.PaymentInstallments?.Where(x=>!x.IsPaid).ToList();
            if (installments != null && installments.Any())
            {
                foreach (var i in installments)
                {
                    i.Notes = Messages<Rental>.RentalCancelled;
                    await _installmentRepository.UpdateAsync(i);
                }
            }

            return Unit.Value;
        }
    }
}
