namespace Domain.Entities
{
    public class PaymentInstallment
    {
        public int PaymentInstallmentId { get; set; }

        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        public DateTime DueDate { get; set; }      // Vade
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidAt { get; set; }

        public string? Notes { get; set; }
        public int? OverdueDays { get; set; }
        public decimal? TotalPenalty { get; set; }
    }
}
