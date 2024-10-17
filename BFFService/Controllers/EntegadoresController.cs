using BFFService.Models;
using BFFService.Ressources;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFFService.Controllers
{
    [Route("entregadores")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "2")]
    public class EntegadoresController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryMan;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly Response _badRequestResponse;

        public EntegadoresController(IDeliveryManService deliveryMan)
        {
            _deliveryMan = deliveryMan;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _badRequestResponse = new Response { Content = new { Mensagem = Messages.IvalidData } };

        }

        /// <summary>
        /// Cadastrar entregador
        /// </summary>
        /// <param name="entregador"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDeliveryManCommand command)
        {
            var response = await _deliveryMan.PostEntregadores(command);
            if(!response.IsSuccessStatusCode)
            {
                return BadRequest(_badRequestResponse);
            }
            return Created();
        }

        /// <summary>
        /// Adiciona a imagem da CNH de um entregador.
        /// </summary>
        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> PostCnh(string id, [FromBody] byte[] imagemCnh)
        {
            var response = await _deliveryMan.PostCnh(id, imagemCnh);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(_badRequestResponse);
            }
            return Created();
        }
    }
}