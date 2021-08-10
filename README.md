# Dapr examples 
Some experiments around [Dapr](https://dapr.io/) to learn.
More detail about this examples and how they work on [my blog](https://elguerre.com/tag/dapr/).

| Samples                                      ||
|----------------------------------------------|---|
| [1. Service Invocation](./ServiceInvocation) and [Service To Service](./ServiceToService) | Demostrate call from service (proxy) to another named as "backend" |
| [2. State Management](./StateManagement)     | Demostrates use of state stores and secrets using Azure Redis and also Azure Storage Table |
| [3. Publish/Subscribe](./PublishSubscribe)   | Demostrates how pub & subs work using Azure Service Bus|
| [4. Bindings](./Bindings)                    | Demostrates how input and ouput bingins work using Azure Storage Queue|
| [5. All Together App](./AllTogetherApp)      | Demostrates how two services and one web site work together. Using docker-compose and also in Kubernetes|
| [6. Actor](./Actor)              | Demostrates creating virtual actors that encapsulate code andstate.|
| [7. ASP.NET Core](./AspNetCore)  | Demostrates ASP.NET Core integration with Dapr by creating Controllers, Routes and other samples |

**Dapr for .NET Developers** is a [great book](https://aka.ms/dapr-ebook) to deep dive.

*This repository contains a samples that highlight the Dapr .NET SDK capabilities from: [DotNet SDK samples](https://github.com/dapr/dotnet-sdk/tree/master/examples)*
