using Application.Features.MediatR.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Istatistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _mediator.Send(new GetRentalStatisticsQuery());
            return Ok(result);
        }

    }
}
