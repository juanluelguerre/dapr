using System.Timers;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;

namespace Samples.Client
{
    public class SecretStoreManagement : Example
    {
        private static readonly string storeName = "secretstore";        
        private static readonly string key = "connectionString";
        public override string DisplayName => "Using the Secret Store";

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var client = new DaprClientBuilder().Build();

            // var all = await client.GetBulkSecretAsync(storeName, cancellationToken: cancellationToken);
            var dic = await client.GetSecretAsync(storeName, key, cancellationToken: cancellationToken);

            if (dic.TryGetValue(key, out var connString))
                Console.WriteLine($"Secret found: '{connString}'");
            else
                Console.WriteLine("Secret not found !");

            //return Task.CompletedTask;
        }
    }
}