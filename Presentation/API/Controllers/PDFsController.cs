using Application.Features.MediatR.Rentals.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PDFsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("GenerateReport")]
        public async Task<IActionResult> GenerateReport([FromQuery] int id)
        {
            var path = await _mediator.Send(new DownloadRentalReportCommand(id));
            return Ok(new { path });
        }
    }
}
