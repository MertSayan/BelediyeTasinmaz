using Application.Constans;
using Application.Features.MediatR.Properties.Commands;
using Application.Features.MediatR.Properties.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FiltreliListele")]
        public async Task<IActionResult> GetAllProperty([FromQuery]GetAllPropertyByFilterQuery query)
        {
            var values = await _mediator.Send(query);
            return Ok(values);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProperty(CreatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Property>.EntityAdded);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProperty(UpdatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Property>.EntityUpdated);
        }
    }
}
