using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rental
    {
        public int RentalId { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public string CitizenNationalId { get; set; }
        public string CitizenPhoneNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; } // Enum: Weekly, Monthly
        public decimal PaymentAmount { get; set; }

        public string ContractFilePath { get; set; } // PDF dosya yolu

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<PaymentInstallment> PaymentInstallments { get; set; }
    }
}
