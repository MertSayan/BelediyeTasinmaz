using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PropertyInterface
{
    public interface IPropertyRepository:IRepository<Property>
    {
        Task<List<Property>> GetPropertiesWithFiltersAsync(PropertyType? type, string? region, double? sizeSqm, PropertyStatus? status,string? name);
    }
}
