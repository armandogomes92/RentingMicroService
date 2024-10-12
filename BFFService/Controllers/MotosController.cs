using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers;

[ApiController]
[Route("motos")]
public class MotosController : ControllerBase
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly IMotorcycleService _rest = RestService.For<IMotorcycleService>("https://localhost:5002");
    public MotosController(IMotorcycleService motorcycleService)
    {
        _motorcycleService = motorcycleService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Moto moto)
    {
        return Ok(await _rest.Post(moto));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _rest.GetMotos());
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> Put(string id, [FromBody] string placa)
    {
        return Ok(await _rest.Put(id, placa));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _rest.GetById(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _rest.Delete(id));
    }
}