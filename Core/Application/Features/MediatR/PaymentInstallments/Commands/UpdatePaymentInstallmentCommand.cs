using MediatR;

namespace Application.Features.MediatR.PaymentInstallments.Commands
{
    public class UpdatePaymentInstallmentCommand:IRequest<int>
    {
        public UpdatePaymentInstallmentCommand(int rentalId, decimal percent)
        {
            RentalId = rentalId;
            Percent = percent;
        }

        public int RentalId { get; set; }
        public decimal Percent { get; set; }
    }
}
