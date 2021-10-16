using CloudNative.CloudEvents;
using Dapr.AzureFunctions.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hello_functions
{
    public static class EmailSender
    {
        [FunctionName("EmailSender")]
        public static async Task Run(
            [DaprTopicTrigger("pubsub", Topic = "greatings")] CloudEvent @event,            
            [DaprSecret("secretstore", "sendgridKey")] IDictionary<string, string> secret,
            [DaprBinding(BindingName = "mail", Operation = "create")] IAsyncCollector<DaprBindingMessage> msg,
            [DaprInvoke(AppId = "webapp1", MethodName = "OneName", HttpVerb = "post")] IAsyncCollector<InvokeMethodParameters> output,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message from Service Bus for '{@event.Data}'");

            
            await output.AddAsync(new InvokeMethodParameters() { Body = $"User '{@event.Data}' has beed add to the system....." });

            await msg.AddAsync(new DaprBindingMessage($"User '{@event.Data}' has beed add to the system....."));

            // log.LogInformation($"And here the secret: {secret["azureStorageKey"]}");

        }
    }
}
