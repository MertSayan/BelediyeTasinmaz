using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Properties.Commands
{
    public class CreatePropertyCommand:IRequest<Unit>
    {
        public string Name { get; set; }
        public PropertyType Type { get; set; } // Enum: Garden, Shop, etc.
        public string Region { get; set; }     // Mahalle / semt / ilçe
        public double? SizeSqm { get; set; }   // Opsiyonel: m²
        public string Description { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public PropertyStatus Status { get; set; } //enum
        public int CreatedByUserId { get; set; }
    }
}
