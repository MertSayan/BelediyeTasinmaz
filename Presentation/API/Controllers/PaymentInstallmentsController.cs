using Application.Features.MediatR.PaymentInstallments.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentInstallmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentInstallmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsPaid(MarkInstallmentAsPaidCommand command)
        {
            var result=await _mediator.Send(command);
            if(!result)
                return NotFound("Installment not found or already paid");
            return Ok("Ödendi olarak işaretlendi");
        }
    }
}
