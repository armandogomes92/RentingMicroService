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
            return Ok(await _motorcycleService.Post(moto));
        }

        /// <summary>
        /// Obtém todas as motos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _motorcycleService.GetMotos());
        }

        /// <summary>
        /// Atualiza a placa de uma moto.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] string placa)
        {
            return Ok(await _motorcycleService.Put(id, placa));
        }

        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _motorcycleService.GetById(id));
        }

        /// <summary>
        /// Deleta uma moto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _motorcycleService.Delete(id));
        }
    }
}