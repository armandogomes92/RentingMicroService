using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Application.Handlers.DeliveryMan.Queries;
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
    public async Task<IActionResult> Create([FromBody] CreateDeliveryManCommand command)
    {
        var response = await _mediator.Send(command);
        var mensagemProperty = response.Content?.GetType().GetProperty("Mensagem");

        if (mensagemProperty != null)
        {
            var mensagemValue = mensagemProperty.GetValue(response.Content) as string;
            if (mensagemValue == Messages.IdentificadorExists || mensagemValue == Messages.CnpjExists || mensagemValue == Messages.CnhNumberExists)
            {
                return NotFound(response.Content);
            }
        }

        return StatusCode(201);
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> Update(string id, [FromBody] byte[] imagemCnh)
    {
        UpdateDeliveryManCommand command = new UpdateDeliveryManCommand
        {
            Identificador = id,
            ImagemCnh = imagemCnh
        };
        var response = await _mediator.Send(command);
        var mensagemProperty = response.Content?.GetType().GetProperty("Mensagem");

        if (mensagemProperty != null)
        {
            var mensagemValue = mensagemProperty.GetValue(response.Content) as string;
            if (mensagemValue == Messages.DeliveryManNotFound)
            {
                return BadRequest(response.Content);
            }
        }

        return StatusCode(201);
    }

    [HttpGet("{id}/cnh-tipo")]
    public async Task<IActionResult> GetCnh(string id)
    {
        var response = await _mediator.Send(new GetCategoryOfDeliveryManQuery { Identificador = id });

        if (!response.Content!.ToString()!.ToUpper().Contains('A'))
        {
            return Ok(response.Content);
        }
        return Ok(response.Content);
    }
}