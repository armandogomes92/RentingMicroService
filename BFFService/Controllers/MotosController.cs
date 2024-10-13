using BFFService.Models;
using BFFService.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BFFService.Controllers
{
    [Route("motos")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "1")]
    public class MotosController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;
        private readonly IMotorcycleService _rest = RestService.For<IMotorcycleService>("https://localhost:5002");

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
            return Ok(await _rest.Post(moto));
        }

        /// <summary>
        /// Obtém todas as motos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _rest.GetMotos());
        }

        /// <summary>
        /// Atualiza a placa de uma moto.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] string placa)
        {
            return Ok(await _rest.Put(id, placa));
        }

        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _rest.GetById(id));
        }

        /// <summary>
        /// Deleta uma moto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _rest.Delete(id));
        }
    }
}