using MediatR;

namespace Application.Features.MediatR.Rentals.Commands
{
    public class DownloadRentalReportCommand : IRequest<string>
    {
        public int RentalId { get; set; }

        public DownloadRentalReportCommand(int rentalId)
        {
            RentalId = rentalId;
        }
    }
}
