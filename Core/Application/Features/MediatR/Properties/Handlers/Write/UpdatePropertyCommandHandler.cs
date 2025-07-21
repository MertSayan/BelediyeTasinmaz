using Application.Features.MediatR.Properties.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.MediatR.Properties.Handlers.Write
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Unit>
    {
        private readonly IRepository<Property> _repository;
        private readonly IMapper _mapper;
        public UpdatePropertyCommandHandler(IRepository<Property> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property=await _repository.GetByIdAsync(request.PropertyId);
            _mapper.Map(request, property);
            property.UpdatedAt = DateTime.Now;
            await _repository.UpdateAsync(property);
            return Unit.Value;
        }
    }
}
