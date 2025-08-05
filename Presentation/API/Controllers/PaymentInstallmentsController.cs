using Application.Features.MediatR.PaymentInstallments.Commands;
using MediatR;
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

        [HttpPut]
        public async Task<IActionResult> MarkAsPaid(MarkInstallmentAsPaidCommand command)
        {
            var result=await _mediator.Send(command);
            if(!result)
                return NotFound("Installment not found or already paid");
            return Ok("Ödendi olarak işaretlendi");
        }
        [HttpPut("YıllıkZam")]
        public async Task<IActionResult> UpdateAmount(UpdatePaymentInstallmentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
