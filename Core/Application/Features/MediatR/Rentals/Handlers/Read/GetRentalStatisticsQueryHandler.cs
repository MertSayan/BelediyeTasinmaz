using Application.Features.MediatR.Rentals.Queries;
using Application.Features.MediatR.Rentals.Results;
using Application.Interfaces;
using Application.Interfaces.RentalInterface;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Read
{
    public class GetRentalStatisticsQueryHandler : IRequestHandler<GetRentalStatisticsQuery, GetRentalStatisticsQueryResult>
    {
        private readonly IRentalStatisticRepository _statisticRepository;

        public GetRentalStatisticsQueryHandler(IRentalStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task<GetRentalStatisticsQueryResult> Handle(GetRentalStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _statisticRepository.GetStatisticsAsync();
        }
    }
}
