using Application.Features.MediatR.Rentals.Commands;
using Application.Interfaces.RentalInterface;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Write
{
    internal class DownloadRentalReportCommandHandler : IRequestHandler<DownloadRentalReportCommand, string>
    {
        private readonly IRentalReportService _reportService;

        public DownloadRentalReportCommandHandler(IRentalReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<string> Handle(DownloadRentalReportCommand request, CancellationToken cancellationToken)
        {
            return await _reportService.GenerateReportAsync(request.RentalId);
        }
    }
}
