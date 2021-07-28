using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Samples.Client
{
    [ApiController]
    [Route("[Controller]")]
    public class WeatherForecastController : ControllerBase
    {       
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Topic("pubsub","forecast")]
        [HttpPost]        
        public async Task<ActionResult> Get([FromBody] WeatherForecast weatherForecast)
        {
            _logger.LogInformation($"Forecast for today ({weatherForecast.Date.DayOfWeek}) " +
                $"is: {weatherForecast.TemperatureC}C. " +
                $"Take care about the {weatherForecast.Summary}.");
            
            return await Task.FromResult(Ok(weatherForecast));           
        }
    }
}
