using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Results
{
    public class GetActiveRentalsByCitizenIdQueryResult:IRequest
    {
        public int RentalId { get; set; }
        public string PropertyName { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Region { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<InstallmentDto> Installments { get; set; }
    }
}
