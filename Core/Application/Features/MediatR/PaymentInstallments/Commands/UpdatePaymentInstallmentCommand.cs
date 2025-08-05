using MediatR;

namespace Application.Features.MediatR.PaymentInstallments.Commands
{
    public class UpdatePaymentInstallmentCommand:IRequest<int>
    {
        public int RentalId { get; set; }
        public decimal Percent { get; set; }
    }
}
