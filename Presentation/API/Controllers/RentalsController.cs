using Application.Constans;
using Application.Features.MediatR.Rentals.Commands;
using Application.Features.MediatR.Rentals.Queries;
using Azure.Core.Extensions;
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

        [HttpGet("KiralamalariGör")]
        public async Task<IActionResult> GetRentalsByFilter([FromQuery] GetRentalsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("KiralamaGeçmişi")]
        public async Task<IActionResult> GetRentalHistory(int propertyId)
        {
            var result = await _mediator.Send(new GetRentalHistoryByPropertyIdQuery(propertyId));
            return Ok(result);
        }
        [HttpGet("TcKimliğeGöreAktifKiralamalar")]
        public async Task<IActionResult> GetActiveRentalsByTc(string citizenId)
        {
            var result = await _mediator.Send(new GetActiveRentalsByCitizenIdQuery(citizenId));
            return Ok(result);
        }

        [HttpPost("Kirala")]
        public async Task<IActionResult> CreateRental(CreateRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Rental>.EntityAdded);
        }
        [HttpPut("Kiralamaİptali")]
        public async Task<IActionResult> CancelRental(CancelRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Rental>.RentalCancelled);
        }
        [HttpPut("KiralamayıUzat")]
        public async Task<IActionResult> UpdateEndDate(ExtendRentalCommand command)
        {
            await _mediator.Send(command);
            return Ok(Messages<Rental>.EntityUpdated);
        }
    }
}
