# Service to Service with Dapr SDK

This sample demonstrates how to use Dapr SDK in a Service to Service call and also debuging it using Visual Studio 2019/2022 thanks to: https://github.com/man-group/dapr-sidekick-dotnet

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

## Visual Studio 2019/2022
```
<ItemGroup>
  <PackageReference Include="Man.Dapr.Sidekick.AspNetCore" Version="1.1.0" />
</ItemGroup>
```
Next modify the ConfigureServices method in Startup.cs as follows:

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    // Add Dapr Sidekick
    services.AddDaprSidekick(Configuration);
}
```

