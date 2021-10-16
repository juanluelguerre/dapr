using Dapr.AzureFunctions.Extension;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace hello_functions
{
    public static class QueueReader
    {
        [FunctionName("QueueReader")]
        public static async Task Run(
            [DaprBindingTrigger(BindingName = "queuebinding")] Data data,
            [DaprState("statestore", Key = "{data.Id}")] IAsyncCollector<string> state,
            [DaprPublish(PubSubName = "pubsub", Topic = "greatings")] IAsyncCollector<string> pubsub,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed a item queue");

            await state.AddAsync(data.Name);
            await pubsub.AddAsync(data.Name);

            log.LogInformation($"C# Queue trigger function processed: {data.Name}");            
        }
    }

}
