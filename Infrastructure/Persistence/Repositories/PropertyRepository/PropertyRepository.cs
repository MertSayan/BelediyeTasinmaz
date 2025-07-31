using Application.Interfaces.PropertyInterface;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.PropertyRepository
{
    public class PropertyRepository:Repository<Property>,IPropertyRepository
    {
        private readonly HobiContext _context;
        public PropertyRepository(HobiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Property>> GetPropertiesWithFiltersAsync(PropertyType? type, string? region, double? sizeSqm, PropertyStatus? status, string? name, string? blockNumber, string? parcelNumber)
        {
            var query = _context.Properties
                .Include(x=>x.CreatedByUser)
                .Include(x=>x.UpdatedByUser)
                .AsQueryable();

            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);

            if (!string.IsNullOrEmpty(region))
                query = query.Where(r => r.Region.Contains(region));

            if (sizeSqm.HasValue)
                query = query.Where(r => r.SizeSqm == sizeSqm.Value);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value); // veya r.Status == status

            if (!string.IsNullOrEmpty(name))
                query = query.Where(r => r.Name.Contains(name));

            if(!string.IsNullOrEmpty(blockNumber))
                query=query.Where(r=>r.BlockNumber==blockNumber);

            if(!string.IsNullOrEmpty(parcelNumber))
                query=query.Where(r=>r.ParcelNumber==parcelNumber);

            return await query.ToListAsync();
        }


    }
}
