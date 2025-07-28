using Application.Features.MediatR.Properties.Results;
using MediatR;

namespace Application.Features.MediatR.Properties.Queries
{
    public class GetPropertyByIdQuery:IRequest<GetPropertyByIdQueryResult>
    {
        public int PropertId { get; set; }

        public GetPropertyByIdQuery(int propertId)
        {
            PropertId = propertId;
        }
    }
}
