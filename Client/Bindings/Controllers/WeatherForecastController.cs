using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingingApi.Controllers
{
    [ApiController]
    [Route("bindingeventdemo")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void ProcessEvent(WeatherForecast forecast)
        {
            _logger.LogInformation("Binding event (demo1) received from Azure Storage Queue !!!");

            _logger.LogInformation($"-> Today ({forecast.Date.DayOfWeek}) will have {forecast.TemperatureC}C.");
        }
    }
}
