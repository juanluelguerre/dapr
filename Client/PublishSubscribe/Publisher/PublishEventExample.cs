// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;

namespace Samples.Client
{
    public class PublishEventExample : Example
    {
        private static readonly string pubsubName = "pubsub";

        public override string DisplayName => "Publishing Events";

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var client = new DaprClientBuilder().Build();
            
            string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();
            var eventData = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            await client.PublishEventAsync(pubsubName, 
                                            topicName: "forecast",
                                            eventData,
                                            cancellationToken);
            Console.WriteLine("Published forecast event!");
        }

        private class Widget
        {
            public string? Size { get; set; }
            public string? Color { get; set; }
        }
    }
}
