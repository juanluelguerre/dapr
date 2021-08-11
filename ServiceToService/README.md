# Service to Service with Dapr SDK

This sample demonstrates how to use Dapr SDK in a Service to Service call.

You can read more about it on the blog post "[Introducción a Dapr .NET SDK y "Service-to-service invocation"](https://elguerre.com/2021/08/05/introduccion-a-dapr-net-sdk-y-service-to-service-invocation/)".

# Try

## Manual
```
dapr run --app-id proxy --app-port 6001 --dapr-http-port 3501 -- dotnet run -p ./WeatherForecastProxyService/WeatherForecastProxyService.csproj
dapr run --app-id backend --app-port 5001 --dapr-http-port 3500 -- dotnet run -p ./WeatherForecastService/WeatherForecastService.csproj
```

## Script
You can start proxy and backend dapr sidecars using `.\start.ps1` (it needs Windows Terminal).

When both sidecars are running use `client.http` to make a request to the proxy service which calls the backend service.

See name resolution by changing the port from `http://localhost:3500` to `http://localhost:3501`, showing that calling any sidecar routes correctly the call.

---
Thanks @laurentKempe to make me easy the way to learn Dapr !
