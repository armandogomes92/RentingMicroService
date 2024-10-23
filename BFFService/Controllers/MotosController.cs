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
            if ((int)response.StatusCode != 201)
            {
                var motos = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<object>(motos, _jsonSerializerOptions);
                return StatusCode((int)response.StatusCode, result!);
            }
            return StatusCode((int)response.StatusCode);
        }

        /// <summary>
        /// Obt�m todas as motos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? placa)
        {
            var response = await _motorcycleService.GetMotos(placa);
            string motos = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(motos, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result!);
        }

        /// <summary>
        /// Atualiza a placa de uma moto.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcycleCommand command)
        {
            var response = await _motorcycleService.Put(id, command);
            string placaReturn = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(placaReturn, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result!);
        }

        /// <summary>
        /// Obt�m uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _motorcycleService.GetById(id);
            string moto = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(moto, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result);
        }

        /// <summary>
        /// Deleta uma moto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _motorcycleService.Delete(id);
            string moto = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode != 200)
            {
                var result = JsonSerializer.Deserialize<object>(moto, _jsonSerializerOptions);
                return StatusCode((int)response.StatusCode, result!);
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}