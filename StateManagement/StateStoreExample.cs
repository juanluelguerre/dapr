// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;

namespace Samples.Client
{
    public class StateStoreExample : Example
    {
        private static readonly string stateKeyName = "Widget";
        private static readonly string storeName = "statestore";

        public override string DisplayName => "Using the State Store";

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var daprClient = new DaprClientBuilder().Build();

            var state = new Widget() { Size = "small", Color = "yellow" };

            await daprClient.SaveStateAsync(storeName,
                                stateKeyName,
                                state,                                
                                cancellationToken: cancellationToken);
            Console.WriteLine("Saved State!");

            state = await daprClient.GetStateAsync<Widget>(storeName,
                                                    stateKeyName,
                                                    cancellationToken: cancellationToken);
            if (state == null)
            {
                Console.WriteLine("State not found in store");
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
