using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Handlers.Rental.Queries;

namespace RentalMotorcycle.Controllers
{
    [ApiController]
    [Route("aluguel")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> Create([FromBody] CreateRentalRegistryCommand command)
        {
            var response = await _mediator.Send(command);

            if (!(bool)response.Content!)
            {
                return BadRequest(response);
            }

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetRentalRegistryByIdQuery { Identificador = id};
            return Ok(await _mediator.Send(query));
        }

        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRentalRegistryCommand command)
        {
            command.Identificador = id;
            var response = await _mediator.Send(command);

            if (!(bool)response.Content!)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("validar-locacao")]
        public async Task<IActionResult> GetRental([FromBody] CheckMotorcycleIsRentingQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}