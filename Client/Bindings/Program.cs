using Dapr.Client;
using System;
using System.Threading.Tasks;

namespace Bindings
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string bindingName = "bindingdemo1";
            const string operationName = "op1";

            var data = new DataBinding() { Id = 1, Name = "demo" };
            using var daprClient = new DaprClientBuilder().Build();

            // Send (Output Binding)
            // await daprClient.InvokeBindingAsync(bindingName, operationName, data);

            



        }
    }


    public class DataBinding
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
