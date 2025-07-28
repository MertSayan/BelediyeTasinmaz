using Application.Features.MediatR.Rentals.Queries;
using Application.Features.MediatR.Rentals.Results;
using Application.Interfaces.RentalInterface;
using AutoMapper;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Read
{
    public class GetRentalHistoryByPropertyIdQueryHandler : IRequestHandler<GetRentalHistoryByPropertyIdQuery, List<GetRentalHistoryByPropertyIdResult>>
    {
        private readonly IRentalRepository _repository;
        private readonly IMapper _mapper;
        public GetRentalHistoryByPropertyIdQueryHandler(IRentalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetRentalHistoryByPropertyIdResult>> Handle(GetRentalHistoryByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repository.ListByFilterAsync(
                r => r.PropertyId == request.PropertyId,
                r => r.CreatedAt, // CreatedAt'e göre azalan sırada
                r => r.PaymentInstallments);

            return _mapper.Map<List<GetRentalHistoryByPropertyIdResult>>(rentals);
        }
    }
}
