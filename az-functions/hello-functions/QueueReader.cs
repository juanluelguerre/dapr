using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace hello_functions
{
    public static class QueueReader
    {
        [FunctionName("QueueReader")]
        public static void Run(
            [DaprBindingTrigger(BindingName="queuebinding")]string queueItem,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {queueItem}");
        }
    }
}
