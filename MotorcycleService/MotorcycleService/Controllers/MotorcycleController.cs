using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Domain.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MotorcycleService.Controllers;

[ApiController]
[Route("motos")]
public class MotorcycleController : ControllerBase
{
    private readonly IMediator _mediator; 
    private readonly JsonSerializerOptions _jsonSerializerOptions;


    public MotorcycleController(IMediator mediator)
    {
        _mediator = mediator;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] CreateMotorcycleCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.Content is not bool result || !(bool)response.Content!)
        { 
            return BadRequest(response.Content);
        }

        return StatusCode(201);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] string? placa)
    {
        var query = new GetMotorcyclesQuery { Placa = placa };
        var result = await _mediator.Send(query);
        return Ok(result.Content);
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMotorcycleCommand command)
    {
        command.Identificador = id;
        var response = await _mediator.Send(command);

        if (response.Content is not bool result || !(bool)response.Content!)
        {
            return BadRequest(response.Content);
        }
        var responseMessage = new Response { Content = new { Mensagem = Messages.UpdatePlate } };
        return Ok(responseMessage.Content);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetMotorcycleByIdQuery { Id = id });
        var mensagemProperty = result.Content?.GetType().GetProperty("Mensagem");

        if (mensagemProperty != null)
        {
            var mensagemValue = mensagemProperty.GetValue(result.Content) as string;
            if (mensagemValue == Messages.MotorcycleNotFound)
            {
                return NotFound(result.Content);
            }
        }
        return Ok(result.Content);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _mediator.Send(new DeleteMotorcycleByIdCommand { Id = id});

        if (!(bool)response.Content!)
        {
            var result = new Response { Content = new { Mensagem = Messages.MotorcycleNotFound } };
            return BadRequest(result.Content);
        }
        return Ok();
    }
}