using BFFService.Models;
using BFFService.Ressources;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFFService.Controllers
{
    [Route("locacao")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "3")]
    public class LocaçãoController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public LocaçãoController(IRentalService rentalService)
        {
            _rentalService = rentalService;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
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
                return Created();
            }

            string rent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(rent, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result!.Content);
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _rentalService.Get(id);
            string rent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(rent, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result!.Content);
        }

        /// <summary>
        /// Atualiza a data de devolução de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] Locacao locacao)
        {

            var response = await _rentalService.Put(id, locacao);
            string rent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var badResult = new Response { Content = new { Mensagem = Messages.IvalidData } };
                return BadRequest(badResult.Content);
            }
            var result = new Response { Content = new { Mensagem = Messages.ReturneredDate } };
            return Ok(result.Content);
        }
    }
}