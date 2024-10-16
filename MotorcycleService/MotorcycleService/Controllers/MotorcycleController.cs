using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Domain.Resources;

namespace MotorcycleService.Controllers;

[ApiController]
[Route("motos")]
public class MotorcycleController : ControllerBase
{
    private readonly IMediator _mediator;

    public MotorcycleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> Create([FromBody] CreateMotorcycleCommand command)
    {
        var response = await _mediator.Send(command);

        if (!(bool)response.Content!)
        {
            return BadRequest(response.Messagem);
        }

        return Created();
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] string? placa)
    {
        var query = new GetMotorcyclesQuery { Placa = placa };
        var result = await _mediator.Send(query);
        return Ok(result.Content);
    }

    [HttpPut("{id}/placa")]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMotorcycleCommand command)
    {
        command.Identificador = id;
        var response = await _mediator.Send(command);

        if (!(bool)response.Content!)
        {
            return BadRequest(response.Messagem);
        }
        return Ok(response.Messagem);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    [ProducesResponseType(typeof(Response), 404)]
    public async Task<IActionResult> GetById(string id)
    {
        var moto = await _mediator.Send(new GetMotorcycleByIdQuery { Id = id });
        if (moto == null)
        {
            return NotFound(moto);
        }
        return Ok(moto);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _mediator.Send(new DeleteMotorcycleByIdCommand { Id = id});

        if (!string.IsNullOrEmpty(response.Messagem))
        {
            return BadRequest(response.Messagem);
        }
        return Ok();
    }
}