using Application.Features.MediatR.Properties.Queries;
using Application.Features.MediatR.Properties.Results;
using Application.Interfaces.PropertyInterface;
using AutoMapper;
using MediatR;

namespace Application.Features.MediatR.Properties.Handlers.Read
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdQueryResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<GetPropertyByIdQueryResult> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _propertyRepository.GetByIdAsync(request.PropertId);
            return _mapper.Map<GetPropertyByIdQueryResult>(values);
        }
    }
}
