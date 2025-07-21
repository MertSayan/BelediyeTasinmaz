using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MediatR.Properties.Commands
{
    public class UpdatePropertyCommand:IRequest<Unit>
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; } // Enum: Garden, Shop, etc.
        public string Region { get; set; }     // Mahalle / semt / ilçe
        public double? SizeSqm { get; set; }   // Opsiyonel: m²
        public string Description { get; set; }
        public PropertyStatus Status { get; set; }
        public int? UpdatedByUserId { get; set; }
    }
}
