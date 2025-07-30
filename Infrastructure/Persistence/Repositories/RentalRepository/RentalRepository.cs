using Application.Interfaces.RentalInterface;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.RentalRepository
{
    public class RentalRepository:Repository<Rental>,IRentalRepository
    {
        private readonly HobiContext _context;
        public RentalRepository(HobiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Rental>> GetAllRentalForBackgroundService()
        {
            return await _context.Rentals
                .Include(r => r.Property)
                .ToListAsync();
        }

        public async Task<List<Rental>> GetRentalsWithFiltersAsync(PropertyType? type, string? region, string? citizenTc, DateTime? start, DateTime? end)
        {
            var query = _context.Rentals
                .Include(r => r.CreatedByUser)
                .Include(r => r.Property)
                .Include(r => r.PaymentInstallments)
                .AsQueryable();

            if (type.HasValue)
                query = query.Where(r => r.Property.Type == type.Value);

            if (!string.IsNullOrWhiteSpace(region))
            {
                query = query.Where(r => r.Property.Region.ToLower().Contains(region.ToLower()));
            }

            if (!string.IsNullOrEmpty(citizenTc))
                query = query.Where(r => r.CitizenNationalId == citizenTc);

            if (start.HasValue)
                query = query.Where(r => r.StartDate >= start.Value);

            if (end.HasValue)
                query = query.Where(r => r.EndDate <= end.Value);

            return await query.ToListAsync();
        }

    }
}
