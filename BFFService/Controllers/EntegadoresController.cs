using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class EntegadoresController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryMan;
        private readonly IDeliveryManService _rest = RestService.For<IDeliveryManService>("https://localhost:5003");

        public EntegadoresController(IDeliveryManService deliveryMan)
        {
            _deliveryMan = deliveryMan;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entregadores moto)
        {

            return Ok(await _rest.PostEntregadores(moto));
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> Get(string id, [FromBody] byte[] imagens)
        {
            return Ok(await _rest.PostCnh(id, imagens));
        }
    }
}
