using Dapr.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace WeatherForecastProxyService
{
    public class WeatherForecastInvokeClient : IWeatherForecastClient
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger _logger;

        public WeatherForecastInvokeClient(DaprClient daprClient, ILogger<WeatherForecastInvokeClient> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int count)
        {
            _logger.LogInformation($"Using DaprClient to invoke/call Service (Backend) !");

            var weatherForecasts =
                await _daprClient.InvokeMethodAsync<List<WeatherForecast>>(HttpMethod.Get, "backend", "weatherforecast");

            return weatherForecasts?.Take(count);
        }
    }
}