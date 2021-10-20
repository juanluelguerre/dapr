using CloudNative.CloudEvents;
using Dapr.AzureFunctions.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hello_functions
{
public static class EmailSender
{
    [FunctionName("EmailSender")]
    public static async Task<IActionResult> Run(
        [DaprTopicTrigger("pubsub", Topic = "greatings")] CloudEvent @event,
        [DaprSecret("secretstore", "sampleKey")] IDictionary<string, string> secret,
        [DaprBinding(BindingName = "mail", Operation = "create")] IAsyncCollector<DaprBindingMessage> mailer,
        // TODO: Issue: https://github.com/dapr/azure-functions-extension/issues/73
        // [DaprInvoke(AppId = "webapp1", MethodName = "OneName", HttpVerb = "post")] IAsyncCollector<InvokeMethodParameters> invoke,
        ILogger log)
    {
        log.LogInformation($"C# ServiceBus topic trigger function initiated");

        var msg = $"Just a sample: User '{@event.Data}' are using '{secret["sampleKey"]}' key.";

        // TODO: Issue: https://github.com/dapr/azure-functions-extension/issues/73
        // var content = new InvokeMethodParameters()
        // {
        //     Body = msg
        // };
        // await invoke.AddAsync(content);

        await mailer.AddAsync(new DaprBindingMessage(msg));

        log.LogInformation($"Email Sender end !");

        return new OkResult();
    }
}
}
