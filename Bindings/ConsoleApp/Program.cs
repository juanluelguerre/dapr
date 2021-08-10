using Dapr.Client;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var client = new DaprClientBuilder().Build();
            client.InvokeBindingAsync("", "", data)
        }
    }
}
