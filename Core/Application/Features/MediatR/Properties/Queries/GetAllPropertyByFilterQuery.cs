using Application.Features.MediatR.Properties.Results;
using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Properties.Queries
{
    public class GetAllPropertyByFilterQuery:IRequest<List<GetAllPropertyByFilterQueryResult>>
    {
        public PropertyType? PropertyType { get; set; }
        public string? Region { get; set; }
        public double? SizeSqm { get; set; }
        public PropertyStatus? PropertyStatus { get; set; }
        public string? Name { get; set; }
        
    }
}
