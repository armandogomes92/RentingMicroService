using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers
{
    [Route("entregadores")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "2")]
    public class EntegadoresController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryMan;

        public EntegadoresController(IDeliveryManService deliveryMan)
        {
            _deliveryMan = deliveryMan;
        }

        /// <summary>
        /// Cadastrar entregador
        /// </summary>
        /// <param name="entregador"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entregadores entregador)
        {
            return Ok(await _deliveryMan.PostEntregadores(entregador));
        }

        /// <summary>
        /// Adiciona a imagem da CNH de um entregador.
        /// </summary>
        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> PostCnh(string id, [FromBody] byte[] imagemCnh)
        {
            return Ok(await _deliveryMan.PostCnh(id, imagemCnh));
        }
    }
}