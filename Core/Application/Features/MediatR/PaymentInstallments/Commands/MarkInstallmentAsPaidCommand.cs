using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MediatR.PaymentInstallments.Commands
{
    public class MarkInstallmentAsPaidCommand:IRequest<bool>
    {
        public int InstallmentId { get; set; }
        public string? Notes { get; set; } 
    }
}
