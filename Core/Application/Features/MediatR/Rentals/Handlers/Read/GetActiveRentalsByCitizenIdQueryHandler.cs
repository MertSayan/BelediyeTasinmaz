using Application.Features.MediatR.Rentals.Queries;
using Application.Features.MediatR.Rentals.Results;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Read
{
    public class GetActiveRentalsByCitizenIdQueryHandler : IRequestHandler<GetActiveRentalsByCitizenIdQuery, List<GetActiveRentalsByCitizenIdQueryResult>>
    {
        private readonly IRepository<Rental> _repository;
        private readonly IMapper _mapper;
        public GetActiveRentalsByCitizenIdQueryHandler(IRepository<Rental> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetActiveRentalsByCitizenIdQueryResult>> Handle(GetActiveRentalsByCitizenIdQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repository.ListByFilterAsync(x => x.IsActive && x.CitizenNationalId == request.CitizenId, x => x.Property ,x=>x.PaymentInstallments);
            return _mapper.Map<List<GetActiveRentalsByCitizenIdQueryResult>>(rentals);
        }
    }
}
