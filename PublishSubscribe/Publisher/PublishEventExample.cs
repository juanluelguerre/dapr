using System.Collections.Generic;
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
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot"
            };

            var rng = new Random();
            var eventData = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
            // TODO: Comment and UnComment to use TTL
            var metadata = new Dictionary<string, string>
            {
               {"ttlInSeconds", "-1"}
            };

            await client.PublishEventAsync(pubsubName,
                                            topicName: "forecast",
                                            eventData,
                                            metadata, /* TODO: Comment and UnComment to use TTL*/
                                            cancellationToken);
            Console.WriteLine("Published forecast event!");
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
            public string? Summary { get; set; }
        }
    }
}
