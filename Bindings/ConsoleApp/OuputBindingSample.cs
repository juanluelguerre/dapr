using Dapr.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class OuputBindingSample : Example
    {
        private static readonly string bindingName = "bindingeventdemo";

        // Allow ouput binding operations: create, get, delete, list
        private static readonly string operation = "create";

        public override string DisplayName => "Using de Output Binding";

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var client = new DaprClientBuilder().Build();

            var data = new WeatherForecast()
            {
                Date = DateTime.Today,
                TemperatureC = 12,
                Summary = "Sunny"
            };

            await client.InvokeBindingAsync(
                            bindingName,
                            operation,
                            data,
                            cancellationToken: cancellationToken);

            Console.WriteLine("Message has been sent !");
        }
    }
}
