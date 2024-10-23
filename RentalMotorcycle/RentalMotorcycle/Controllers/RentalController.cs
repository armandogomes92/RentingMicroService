using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Handlers.Rental.Queries;

namespace RentalMotorcycle.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateRentalRegistryCommand command)
        {
            var response = await _mediator.Send(command);
            var mensagemProperty = response.Content?.GetType().GetProperty("Mensagem");

            if (mensagemProperty != null)
            {
                var mensagemValue = mensagemProperty.GetValue(response.Content) as string;
                if (mensagemValue == Messages.InvalidCnh || mensagemValue == Messages.InvalidData || mensagemValue == Messages.MotorcycleIsRenting)
                {
                    return BadRequest(response.Content);
                }
            }
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetRentalRegistryByIdQuery { Identificador = id};
            var result = await _mediator.Send(query);
            if (result.Content == null)
            {
                var notFoundResult = new Response { Content = new { Messagem = Messages.MotorcycleRentRegistryNotFound } };
                return NotFound(notFoundResult.Content);
            }
            return Ok(result.Content);
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRentalRegistryCommand command)
        {
            command.Identificador = id;
            var response = await _mediator.Send(command);

            var mensagemProperty = response.Content?.GetType().GetProperty("Mensagem");

            if (mensagemProperty != null)
            {
                var mensagemValue = mensagemProperty.GetValue(response.Content) as string;

                if (mensagemValue == Messages.InvalidData)
                {
                    return BadRequest(response.Content);
                }
            }
            return Ok(response.Content);
        }

        [HttpGet("validar-locacao/{id}")]
        public async Task<IActionResult> GetRental( string id)
        {
            var result = await _mediator.Send(new CheckMotorcycleIsRentingQuery { Identificador = id });
            
            return Ok(result.Content);
        }
    }
}