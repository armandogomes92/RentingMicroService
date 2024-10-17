using BFFService.Models;
using BFFService.Ressources;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFFService.Controllers
{
    [Route("motos")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "1")]
    public class MotosController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MotosController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        /// <summary>
        /// Adiciona uma nova moto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Moto moto)
        {
            var response = await _motorcycleService.Post(moto);
            var motos = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = new Response { Content = new { Mensagem = Messages.IvalidData } };
                return BadRequest(result.Content);
            }
            return Created();
        }

        /// <summary>
        /// Obt�m todas as motos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? placa)
        {
            var response = await _motorcycleService.GetMotos(placa);
            string motos = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(motos, _jsonSerializerOptions);

            return Ok(result!.Content);
        }

        /// <summary>
        /// Atualiza a placa de uma moto.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcycleCommand command)
        {
            var response = await _motorcycleService.Put(id, command);
            string placaReturn = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(placaReturn, _jsonSerializerOptions);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result!.Content);
            }

            return Ok();
        }

        /// <summary>
        /// Obt�m uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new Response { Content = Messages.BadFormatRequest });
            }
            var response = await _motorcycleService.GetById(id);

            if ((int)response.StatusCode == 404)
            {
                return NotFound(new Response { Content = Messages.MotorcycleNotFound });
            }

            string moto = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(moto, _jsonSerializerOptions);

            return Ok(result!.Content);
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
                return BadRequest(new Response { Content = Messages.BadFormatRequest });
            }
            return Ok();
        }
    }
}