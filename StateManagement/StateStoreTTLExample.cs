using System.Runtime.CompilerServices;
// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using System.Collections.Generic;

namespace Samples.Client
{
    public class StateStoreTTLExample : Example
    {
        private static readonly int ttlSecs = 5;
        
        private static readonly string stateKeyName = "Widget";
        private static readonly string storeName = "statestore";

        public override string DisplayName => "Using the State Store with TTL";

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var daprClient = new DaprClientBuilder().Build();

            var state = new Widget() { Size = "small", Color = "yellow" };

            var metadata = new Dictionary<string, string>
            {
                // This value ovierride the one that has been define inside componennt (.yaml)
                { "ttlInSeconds", $"{ttlSecs}" } 
            };

            await daprClient.SaveStateAsync(storeName,
                                stateKeyName,
                                state,
                                null,
                                metadata,
                                cancellationToken: cancellationToken);
            Console.WriteLine($"Saved State with TTL = {ttlSecs} !");

            Console.WriteLine($" -> So, you have {ttlSecs} secs to check your State Store and be sure the '{stateKeyName}' key already exists !");

            // Wait for 11 secs
            Task.Delay(11000).Wait();

            Console.WriteLine($" -> [TTL Apply after {ttlSecs} secs] Now, if you check your State Store the state key doesn't exist anymore !");

            state = await daprClient.GetStateAsync<Widget>(storeName,
                                                    stateKeyName,
                                                    cancellationToken: cancellationToken);
            if (state == null)
            {
                Console.WriteLine("State not found in store. Maybe TTL has been finished !");
            }
            else
            {
                Console.WriteLine($"Got State: {state.Size} {state.Color}");
            }

            // TODO: Just Comment to be sure after run the example we can query results !
            // await client.DeleteStateAsync(storeName,
            //                             stateKeyName,
            //                             cancellationToken: cancellationToken);
            // Console.WriteLine("Deleted State!");
        }

        private class Widget
        {
            public string? Size { get; set; }
            public string? Color { get; set; }
        }
    }
}
