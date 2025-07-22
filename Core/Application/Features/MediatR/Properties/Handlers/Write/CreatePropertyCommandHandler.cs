using Application.Features.MediatR.Properties.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Properties.Handlers.Write
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Unit>
    {
        private readonly IRepository<Property> _repository;
        private readonly IMapper _mapper;

        public CreatePropertyCommandHandler(IRepository<Property> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property=_mapper.Map<Property>(request);
            property.CreatedAt = DateTime.Now;
            property.Status=PropertyStatus.Available;
            await _repository.AddAsync(property);
            return Unit.Value;
        }
    }
}
