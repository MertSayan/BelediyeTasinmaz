using Application.Features.MediatR.Rentals.Results;
using Application.Interfaces.RentalInterface;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.RentalRepository
{
    public class RentalStatisticRepository : IRentalStatisticRepository
    {
        private readonly HobiContext _context;

        public RentalStatisticRepository(HobiContext context)
        {
            _context = context;
        }

        public async Task<GetRentalStatisticsQueryResult> GetStatisticsAsync()
        {
            var rentals = await _context.Rentals.Include(r => r.PaymentInstallments)
                .Include(r=>r.Property)
                .ToListAsync();
            var allInstallments = rentals.SelectMany(r => r.PaymentInstallments).ToList();

            var result = new GetRentalStatisticsQueryResult
            {
                TotalRentalCount = rentals.Count,
                NewRentalsThisMonth = rentals.Count(r => r.CreatedAt.Month == DateTime.Now.Month && r.CreatedAt.Year == DateTime.Now.Year),
                ActiveRentals = rentals.Count(r => r.IsActive),
                InactiveRentals = rentals.Count(r => !r.IsActive),
                ExpiringSoonCount = rentals.Count(r =>
                    r.IsActive && r.EndDate.Date >= DateTime.Today && r.EndDate <= DateTime.Today.AddDays(15)),

                TotalPaidInstallments = allInstallments.Count(i => i.IsPaid),
                TotalPaidAmount = allInstallments.Where(i => i.IsPaid).Sum(i => i.Amount),

                UnpaidInstallments = allInstallments.Count(i => !i.IsPaid),
                UnpaidAmount = allInstallments.Where(i => !i.IsPaid).Sum(i => i.Amount),

                OverdueInstallments = allInstallments.Count(i => !i.IsPaid && i.DueDate < DateTime.Today),
                TotalOverdueDays = allInstallments.Where(i => !i.IsPaid && i.DueDate < DateTime.Today)
                                                  .Sum(i => (DateTime.Today - i.DueDate).Days),

                PenaltyAppliedCount = allInstallments.Count(i => i.TotalPenalty.HasValue && i.TotalPenalty > 0),
                TotalPenaltyAmount = allInstallments.Where(i => i.TotalPenalty.HasValue).Sum(i => i.TotalPenalty ?? 0),

                MonthlyInstallmentCounts = allInstallments
                    .Where(i => i.DueDate.Year == DateTime.Now.Year)
                    .GroupBy(i => i.DueDate.ToString("MMMM"))
                    .ToDictionary(g => g.Key, g => g.Count()),

                MonthlyRentalCounts = rentals
                    .Where(r => r.CreatedAt.Year == DateTime.Now.Year)
                    .GroupBy(r => r.CreatedAt.ToString("MMMM"))
                    .ToDictionary(g => g.Key, g => g.Count()),

                MostRentedPropertyType = rentals
                    .GroupBy(r => r.Property.Type)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key.ToString())
                    .FirstOrDefault(),

                RentalDistributionByRegion = rentals
                    .GroupBy(r => r.Property.Region)
                    .ToDictionary(g => g.Key, g => g.Count()),

                ActiveRental = rentals
                    .Where(r => r.IsActive)
                    .Select(r => new ActiveRentalInfoResult
                    {
                        PropertyName = r.Property.Name,
                        CitizenNationalId = r.CitizenNationalId
                    }).ToList(),

                TopCitizens = rentals
                    .GroupBy(r => r.CitizenNationalId)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new TopCitizenResult
                    {
                        NationalId = g.Key,
                        RentalCount = g.Count()
                    }).ToList(),

                AverageRentalDays = rentals.Count > 0
                    ? rentals.Average(r => (r.EndDate - r.StartDate).TotalDays)
                    : 0,

                MaxRentalAmount = allInstallments.Max(i => i.Amount),
                InstallmentsDueToday = allInstallments.Count(i => i.DueDate.Date == DateTime.Today)
            };

            return result;
        }
    }
}
