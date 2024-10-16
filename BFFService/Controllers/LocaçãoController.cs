using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public async Task<IActionResult> Post([FromBody] CreateRentalRegistryCommand command)
        {

            var response = await _rentalService.Post(command);
            if (response.IsSuccessStatusCode)
            {
                var rent = await response.Content.ReadAsStreamAsync();
                return Created();
            }
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = await _rentalService.Get(id);
            if (response.IsSuccessStatusCode)
            {
                var rent = await response.Content.ReadAsStreamAsync();
                return Ok(rent);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        /// <summary>
        /// Atualiza a data de devolução de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] Locacao locacao)
        {

            var response = await _rentalService.Put(id, locacao);
            if (response.IsSuccessStatusCode)
            {
                var rent = await response.Content.ReadAsStreamAsync();
                return Ok();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}