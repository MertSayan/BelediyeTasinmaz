using Application.Constans;
using Application.Features.MediatR.Rentals.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental(CreateRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Rental>.EntityAdded);
        }
    }
}
