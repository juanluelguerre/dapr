# Sample App

This sample app demonstrates how to use [.NET Dapr SDK](https://github.com/dapr/dotnet-sdk) for State, Pub/Sub and Service to Service call.
It leverage [Dapr SideKick](https://github.com/man-group/dapr-sidekick-dotnet) to be able to start easily the Dapr sidecar from your application or services.
It also shows the usage of Jaeger for distributed traces.

It was created from https://github.com/laurentkempe/daprPlayground/tree/master/sampleApp 

* WebSite, a Blazor server application, uses **Dapr State** to retrieve/store a counter state and **Dapr Pub/Sub** to publish an event.
* CounterService, a ASP.NET 5 WebApi, uses Dapr Pub/Sub to **subscribe** to the event and log it.
* WeatherService, the default template for ASP.NET 5 WebApi, just use Dapr SideKick to start it with Dapr.

# Try

## Start Jaeger

    docker run -d --name jaeger -e COLLECTOR_ZIPKIN_HOST_PORT=:9412 -p 16686:16686 -p 9412:9412 jaegertracing/all-in-one:1.22

## Start Sample App

You can start WebSite and CounterService `.\start.ps1` (it needs Windows Terminal), which will also start the Dapr sidecars.


Thanks to https://laurentkempe.com to share.

# Multi-container Dapr application (docker-compose
Sample based on: https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/getting-started#build-a-multi-container-dapr-application 

```
docker compose build
docker compose up
```

```

```