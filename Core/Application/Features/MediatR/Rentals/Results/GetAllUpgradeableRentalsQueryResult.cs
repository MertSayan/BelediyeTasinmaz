namespace Application.Features.MediatR.Rentals.Results
{
    public class GetAllUpgradeableRentalsQueryResult
    {
        public int RentalId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
