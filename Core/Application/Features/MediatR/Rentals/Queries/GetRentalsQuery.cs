using Application.Features.MediatR.Rentals.Results;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MediatR.Rentals.Queries
{
    public class GetRentalsQuery:IRequest<List<GetRentalsQueryResult>>
    {
        public PropertyType? PropertyType { get; set; }
        public string? Region { get; set; }
        public string? CitizenNationalId { get; set; }
        public DateTime? StartDate { get; set; }  
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
