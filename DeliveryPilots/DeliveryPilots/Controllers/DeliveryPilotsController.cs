using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Domain.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryPilots.Controllers;

[ApiController]
[Route("entregadores")]
public class DeliveryPilotsController : ControllerBase
{
    private readonly IMediator _mediator;
    public DeliveryPilotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Response), 201)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryManCommand command)
    {
        var response = await _mediator.Send(command);

        if (!(bool)response.Content!)
        {
            return BadRequest(new Response { Content = new { Messagem = Messages.InvalidData } });
        }

        return Created();
    }

    [HttpPost("{id}/cnh")]
    [ProducesResponseType(typeof(Response), 201)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> Update(string id, [FromBody] byte[] imagemCnh)
    {
        UpdateDeliveryManCommand command = new UpdateDeliveryManCommand
        {
            Identificador = id,
            ImagemCnh = imagemCnh
        };
        var response = await _mediator.Send(command);

        if (!(bool)response.Content!)
        {
            return BadRequest(new Response { Content = new { Messagem = Messages.InvalidData } });
        }

        return Ok(response);
    }

    [HttpGet("{id}/cnh-tipo")]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 404)]
    public async Task<IActionResult> GetCnh(string id)
    {
        var response = await _mediator.Send(new GetCategoryOfDeliveryManQuery { Identificador = id });

        if (!response.Content!.ToString()!.ToUpper().Contains('A'))
        {
            return BadRequest(response.Content);
        }
        return Ok(response.Content);
    }
}