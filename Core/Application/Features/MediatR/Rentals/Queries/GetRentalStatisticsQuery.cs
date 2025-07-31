using Application.Features.MediatR.Rentals.Results;
using MediatR;

namespace Application.Features.MediatR.Rentals.Queries
{
    public class GetRentalStatisticsQuery:IRequest<GetRentalStatisticsQueryResult>
    {
    }
}
