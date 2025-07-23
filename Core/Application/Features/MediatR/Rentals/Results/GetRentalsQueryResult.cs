using MediatR;

namespace Application.Features.MediatR.Rentals.Results
{
    public class GetRentalsQueryResult:IRequest
    {
        public int RentalId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Region { get; set; }
        public string CitizenNationalId { get; set; }
        public string CitizenPhoneNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<InstallmentDto> Installments { get; set; }
        public string CreatedEmployee {  get; set; }

        public bool IsActive => DateTime.Now < EndDate;
    }
    public class InstallmentDto
    {
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
