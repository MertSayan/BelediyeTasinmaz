using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MediatR.Rentals.Results
{
    public class GetRentalStatisticsQueryResult
    {
        // Genel Kiralama
        public int TotalRentalCount { get; set; }
        public int NewRentalsThisMonth { get; set; }
        public int ActiveRentals { get; set; }
        public int InactiveRentals { get; set; }
        public int ExpiringSoonCount { get; set; }

        // Taksitler
        public int TotalPaidInstallments { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public int UnpaidInstallments { get; set; }
        public decimal UnpaidAmount { get; set; }
        public int OverdueInstallments { get; set; }
        public int TotalOverdueDays { get; set; }
        public int PenaltyAppliedCount { get; set; }
        public decimal TotalPenaltyAmount { get; set; }

        // Aylık grafik verileri (Frontend için key-value)
        public Dictionary<string, int> MonthlyInstallmentCounts { get; set; }
        public Dictionary<string, int> MonthlyRentalCounts { get; set; }

        // Grup bazlı
        public string MostRentedPropertyType { get; set; }
        public Dictionary<string, int> RentalDistributionByRegion { get; set; }
        public List<ActiveRentalInfoResult> ActiveRental { get; set; }
        public List<TopCitizenResult> TopCitizens { get; set; }
        //public List<TopEmployeeResult> TopEmployees { get; set; }

        // Ek veriler
        public double AverageRentalDays { get; set; }
        public decimal MaxRentalAmount { get; set; }
        public int InstallmentsDueToday { get; set; }
    }
}
