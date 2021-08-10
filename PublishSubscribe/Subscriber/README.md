# Dapr .NET SDK pub/sub (Subscriber) example

## Prerequisites

- [.NET Core 3.1 or .NET 5+](https://dotnet.microsoft.com/download) installed
- [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
- [Initialized Dapr environment](https://docs.dapr.io/getting-started/install-dapr-selfhost/)
- [Dapr .NET SDK](https://docs.dapr.io/developing-applications/sdks/dotnet/)

## Running the example

To run the sample locally run this command in the Subscriber directory:

```sh
dapr run --app-id subscriber --app-port 5000  -- dotnet run
```
or, to run using **Azure Service Bus**

```sh
dapr run --app-id subscriber --app-port 5000 --components-path ../dapr/components -- dotnet run
```

Folder components include a Dapr component 
```
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: pubsub
  namespace: default
spec:
  type: pubsub.azure.servicebus
  version: v1
  metadata:
  - name: connectionString # Required    
    value: "Endpoint=sb://<SERVICEBUS-NAMESPACE>.servicebus.windows.net/;SharedAccessKeyName=<KEY-NAME>;SharedAccessKey=<KEY>"
```

**Note:** Azure Service Bus have to bee configured prevously.


Running the following command will output a Weather Forecast:

```
== APP == info: subscriber.Controllers.WeatherForecastController[0]
== APP ==       Forecast for today (Wednesday) is: 40C. Take care about the Sweltering.
```

## Publishing Pub/Sub Events

See [PublishEventExample.cs](./PublishEventExample.cs) for an example using the `DaprClient` to publish a pub/sub event.
