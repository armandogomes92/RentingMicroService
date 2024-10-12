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
            return BadRequest(response);
        }

        return Created();
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetMotorcyclesQuery();
        return Ok(await _mediator.Send(query));
    }

    [HttpPut("{id}/placa")]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMotorcycleCommand command)
    {
        var response = await _mediator.Send(command);

        if (!(bool)response.Content!)
        {
            return BadRequest(response);
        }
        return Ok(response);
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
    public async Task<IActionResult> Delete(DeleteMotorcycleByIdCommand command)
    {
        var response = await _mediator.Send(command);

        if (!string.IsNullOrEmpty(response.Messagem))
        {
            return BadRequest(response);
        }
        return Ok();
    }
}