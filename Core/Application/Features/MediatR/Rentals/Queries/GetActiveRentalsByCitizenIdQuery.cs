using Application.Features.MediatR.Rentals.Results;
using MediatR;

namespace Application.Features.MediatR.Rentals.Queries
{
    public class GetActiveRentalsByCitizenIdQuery:IRequest<List<GetActiveRentalsByCitizenIdQueryResult>>
    {
        public string CitizenId { get; set; }

        public GetActiveRentalsByCitizenIdQuery(string citizenId)
        {
            CitizenId = citizenId;
        }
    }
}
