using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BFFService.Controllers
{
    [Route("motos")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "1")]
    public class MotosController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotosController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        /// <summary>
        /// Adiciona uma nova moto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Moto moto)
        {
            var response = await _motorcycleService.Post(moto);
            if (response.IsSuccessStatusCode)
            {
                var motos = await response.Content.ReadAsStreamAsync();
                return Ok(motos);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        /// <summary>
        /// Obtém todas as motos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? placa)
        {
            var response = await _motorcycleService.GetMotos(placa);
            if (response.IsSuccessStatusCode)
            {
                var motos = await response.Content.ReadAsStreamAsync();
                return Ok(motos);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        /// <summary>
        /// Atualiza a placa de uma moto.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcycleCommand command)
        {
            var response = await _motorcycleService.Put(id, command);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var content = await response.Content.ReadAsStreamAsync();
            return Ok(content);
        }

        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _motorcycleService.GetById(id);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var content = await response.Content.ReadAsStreamAsync();

            return Ok(content);
        }

        /// <summary>
        /// Deleta uma moto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _motorcycleService.Delete(id);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var content = await response.Content.ReadAsStreamAsync();
            return Ok(content);
        }
    }
}