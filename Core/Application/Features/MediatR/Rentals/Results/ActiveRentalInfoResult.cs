using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MediatR.Rentals.Results
{
    public class ActiveRentalInfoResult
    {
        public string PropertyName { get; set; }
        public string CitizenNationalId { get; set; }
    }
}
