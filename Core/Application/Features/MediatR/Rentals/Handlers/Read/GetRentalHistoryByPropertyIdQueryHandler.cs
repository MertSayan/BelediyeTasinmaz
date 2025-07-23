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
            var rentals = await _repository.ListByFilterAsync(r => r.PropertyId == request.PropertyId, r => r.PaymentInstallments);

            return _mapper.Map<List<GetRentalHistoryByPropertyIdResult>>(rentals);

            //var result = rentals.Select(r => new GetRentalHistoryByPropertyIdResult
            //{
            //    CitizenNationalId = r.CitizenNationalId,
            //    StartDate = r.StartDate,
            //    EndDate = r.EndDate,
            //    Installments = r.PaymentInstallments.Select(i => new PaymentInstallmentResult
            //    {
            //        DueDate = i.DueDate,
            //        Amount = i.Amount,
            //        IsPaid = i.IsPaid
            //    }).ToList()
            //}).ToList();

            //return result;
        }
    }
}
