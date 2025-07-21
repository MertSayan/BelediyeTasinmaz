using Application.Constans;
using Application.Features.MediatR.Properties.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProperty(CreatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Property>.EntityAdded);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProperty(UpdatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Property>.EntityUpdated);
        }
    }
}
