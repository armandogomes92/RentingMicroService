using Microsoft.AspNetCore.Mvc;
using NotificationService.Service.Services;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("notification")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MongoService _mongoService;
        public WeatherForecastController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mongoService.GetAdminNotificationsAsync());
        }

        [HttpGet("{id}/user")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _mongoService.UserNotificationsByIdAsync(id));
        }
    }
}
