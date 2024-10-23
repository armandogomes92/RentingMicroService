using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        /// <summary>
        /// Adiciona uma nova locação.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentalRegistryCommand command)
        {

            var response = await _rentalService.Post(command);
            if ((int)response.StatusCode != 201)
            {
                string rent = await response.Content.ReadAsStringAsync();
                var resultEncoding = Encoding.UTF8.GetBytes(rent);
                var result = JsonSerializer.Deserialize<string>(resultEncoding, _jsonSerializerOptions);
                return StatusCode((int)response.StatusCode, result);
            }
            return StatusCode((int)response.StatusCode);
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _rentalService.Get(id);
            string rent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(rent, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result);
        }

        /// <summary>
        /// Atualiza a data de devolução de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] Locacao locacao)
        {

            var response = await _rentalService.Put(id, locacao);
            string rent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(rent, _jsonSerializerOptions);

            return StatusCode((int)response.StatusCode, result);
        }
    }
}