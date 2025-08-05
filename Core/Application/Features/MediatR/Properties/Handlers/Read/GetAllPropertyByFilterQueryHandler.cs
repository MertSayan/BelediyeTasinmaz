using Application.Features.MediatR.Properties.Queries;
using Application.Features.MediatR.Properties.Results;
using Application.Interfaces.PropertyInterface;
using AutoMapper;
using MediatR;

namespace Application.Features.MediatR.Properties.Handlers.Read
{
    public class GetAllPropertyByFilterQueryHandler : IRequestHandler<GetAllPropertyByFilterQuery, List<GetAllPropertyByFilterQueryResult>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyByFilterQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPropertyByFilterQueryResult>> Handle(GetAllPropertyByFilterQuery request, CancellationToken cancellationToken)
        {
            var value = await _propertyRepository.GetPropertiesWithFiltersAsync(request.PropertyType,request.Region,request.SizeSqm,request.PropertyStatus,request.Name,request.BlockNumber,request.ParcelNumber);

            return _mapper.Map<List<GetAllPropertyByFilterQueryResult>>(value);
        }
    }
}
