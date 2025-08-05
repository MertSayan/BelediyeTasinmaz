using Application.Features.MediatR.Rentals.Results;
using MediatR;

namespace Application.Features.MediatR.Rentals.Queries
{
    public class GetRentalHistoryByPropertyIdQuery:IRequest<List<GetRentalHistoryByPropertyIdResult>>
    {
        public int PropertyId { get; set; }

        public GetRentalHistoryByPropertyIdQuery(int propertyId)
        {
            PropertyId = propertyId;
        }
    }


}
