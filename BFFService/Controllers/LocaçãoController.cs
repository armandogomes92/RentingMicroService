using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers
{
    [Route("locacao")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "3")]
    public class LocaçãoController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IRentalService _rest = RestService.For<IRentalService>("https://localhost:5001");

        public LocaçãoController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Adiciona uma nova locação.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Locacao locacao)
        {
            return Ok(await _rest.Post(locacao));
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _rest.Get(id));
        }

        /// <summary>
        /// Atualiza a data de devolução de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] DateTime dataDevolucao)
        {
            return Ok(await _rest.Put(id, dataDevolucao));
        }
    }
}