using MediatR;

namespace Application.Features.MediatR.Properties.Results
{
    public class GetAllPropertyByFilterQueryResult:IRequest
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Enum: Garden, Shop, etc.
        public string Status { get; set; }
        public string Region { get; set; }     // Mahalle / semt / ilçe
        public double? SizeSqm { get; set; }   // Opsiyonel: m²
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}
