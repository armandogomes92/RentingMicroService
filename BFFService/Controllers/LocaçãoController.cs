using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers;

[Route("locacao")]
[ApiController]
public class LocaçãoController : ControllerBase
{
    private readonly IRentalService _rentalService;
    private readonly IRentalService _rest = RestService.For<IRentalService>("https://localhost:5001");

    public LocaçãoController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Locacao locacao)
    {
        return Ok(await _rest.Post(locacao));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _rest.Get(id));
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, [FromBody] DateTime dataDevolucao)
    {
        return Ok(await _rest.Put(id, dataDevolucao));
    }
}