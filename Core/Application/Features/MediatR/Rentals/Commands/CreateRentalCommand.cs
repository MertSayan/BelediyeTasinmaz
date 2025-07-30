using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Commands
{
    public class CreateRentalCommand:IRequest<Unit>
    {
        public int PropertyId { get; set; }
        public string CitizenNationalId { get; set; }
        public string CitizenFullName { get; set; }
        public string CitizenPhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; } // Enum: Weekly, Monthly
        public decimal TotalAmount { get; set; } 
        public string ContractFilePath { get; set; } // PDF dosya yolu
        public int CreatedByUserId { get; set; }

    }
}
