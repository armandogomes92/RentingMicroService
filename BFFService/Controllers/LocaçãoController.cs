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
            return Ok(await _rentalService.Post(locacao));
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _rentalService.Get(id));
        }

        /// <summary>
        /// Atualiza a data de devolução de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] DateTime dataDevolucao)
        {
            return Ok(await _rentalService.Put(id, dataDevolucao));
        }
    }
}