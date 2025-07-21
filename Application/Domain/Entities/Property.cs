using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; } // Enum: Garden, Shop, etc.
        public string Region { get; set; }     // Mahalle / semt / ilçe
        public double? SizeSqm { get; set; }   // Opsiyonel: m²

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public PropertyStatus Status { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        // Navigation
        public ICollection<Rental> Rentals { get; set; }
    }
}
