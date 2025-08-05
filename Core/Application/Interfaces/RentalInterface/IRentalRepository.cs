using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.RentalInterface
{
    public interface IRentalRepository:IRepository<Rental>
    {
        Task<List<Rental>> GetRentalsWithFiltersAsync(PropertyType? type,string? region,string? citizenTc,DateTime? start,DateTime? end,bool? isActive);
        Task<List<Rental>> GetAllRentalForBackgroundService();

        Task<Rental> GetRentalWithDetailsAsync(int rentalId); // Property + Installments
        Task UpdateReportPathAsync(int rentalId, string reportPath);
    }
}
