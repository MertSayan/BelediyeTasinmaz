using Application.Features.MediatR.Rentals.Queries;
using Application.Features.MediatR.Rentals.Results;
using Application.Interfaces.RentalInterface;
using AutoMapper;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers.Read
{
    public class GetRentalsQueryHandler : IRequestHandler<GetRentalsQuery, List<GetRentalsQueryResult>>
    {
        private readonly IRentalRepository _repository;
        private readonly IMapper _mapper;

        public GetRentalsQueryHandler(IRentalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetRentalsQueryResult>> Handle(GetRentalsQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _repository.GetRentalsWithFiltersAsync(
                          request.PropertyType, request.Region, request.CitizenNationalId,
                          request.StartDate, request.EndDate,request.IsActive);

            return _mapper.Map<List<GetRentalsQueryResult>>(rentals);
        }
    }
}
