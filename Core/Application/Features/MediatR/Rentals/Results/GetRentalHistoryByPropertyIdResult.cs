namespace Application.Features.MediatR.Rentals.Results
{
    public class GetRentalHistoryByPropertyIdResult
    {
        public string CitizenNationalId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PaymentInstallmentResult> Installments { get; set; }
    }
    public class PaymentInstallmentResult
    {
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public string Notes { get; set; }
    }

}
