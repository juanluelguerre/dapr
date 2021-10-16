//using Dapr.AzureFunctions.Extension;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;

//namespace hello_functions
//{
//    public static class JustInvoker
//    {
//        [FunctionName("JustInvoker")]
//        public static async Task Run(
//            [DaprServiceInvocationTrigger("invokeme")] webapp1,
//            ILogger log)
//        {
//            log.LogInformation($"C# function has been invoked using Dapr");



//https://github.com/dapr/azure-functions-extension/blob/master/samples/dotnet-azurefunction/RetrieveOrder.cs

//            // await webapp1.g(msg);
//        }
//    }
//}
