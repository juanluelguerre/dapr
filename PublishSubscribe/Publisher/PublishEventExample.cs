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
using Newtonsoft.Json;

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
              // {"ttlInSeconds", "-1"},
               
               // https://github.com/dapr/components-contrib/pull/1071
               // https://github.com/dapr/dapr/pull/3604
               //{ "asbsessionId", "123" },
               //{ "MessageId", "{701332E1-B37B-4D29-AA0A-E367906C206E}" },
               //{ "TimeToLive", "90" },
               //{ "CorrelationId", "{701332F3-B37B-4D29-AA0A-E367906C206E}" },
               //{ "SequenceNumber", "12345" },
               //{ "DeliveryCount", "2" },
               //{ "To", "http,//contoso.com" },
               //{ "ReplyTo", "http,//fabrikam.com" },
               //{ "asbsenqueuedTimeUtc", "Sun, 06 Nov 1994 08,49,37 GMT" },
               //{ "asbscheduledEnqueueTimeUtc", "Sun, 06 Nov 1994 08,49,37 GMT" }
               { "ScheduledEnqueueTimeUtc", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy hh,mm,ss") }
               //// {"label", "test-label"},
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
