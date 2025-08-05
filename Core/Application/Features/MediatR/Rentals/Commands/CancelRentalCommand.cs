using MediatR;

namespace Application.Features.MediatR.Rentals.Commands
{
    public class CancelRentalCommand:IRequest<Unit>
    {
        public int RentalId { get; set; }
        public int CancelByUserId { get; set; }
    }
}
