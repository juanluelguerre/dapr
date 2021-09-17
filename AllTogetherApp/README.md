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

### Start individaly:
```
dapr run --app-id weatherservice --app-port 5003, --dapr-http-port 3503, --dapr-grpc-port 50003 --components-path ./components --config ./components/config.yaml -- dotnet run -p ./WeatherService/WeatherService.csproj

dapr run --app-id counterservice --app-port 5002, --dapr-http-port 3502, --dapr-grpc-port 50002 --components-path ./components --config ./components/config.yaml -- dotnet run -p ./CounterService/CounterService.csproj

dapr run --app-id website --app-port 5000 --dapr-http-port 3500 --dapr-grpc-port 50001 --components-path ./components --config ./components/config.yaml -- dotnet run -p ./WebSite/WebSite.csproj
```

### Start easyly, all at once:
You can start WebSite and CounterService `.\start.ps1` (it needs Windows Terminal), which will also start the Dapr sidecars.
Thanks to https://laurentkempe.com to share.

# Multi-container Dapr application (docker-compose
Sample based on: https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/getting-started#build-a-multi-container-dapr-application 

1. Reivew al Dockerfile
2. Remember Dapr must have been initialized using: `dapr init`
3. Run below sentences from command line once you're placed on application root folder
4. Create a new network on docker:
```
docker network create -d bridge alltogether
```
5. Compile and Star up services and Website. Also other dependencies: redis, zipkin, ....
```
docker compose build
docker compose up
```
**Note:** Dapr component map components ports, so we don't need to explicit set ports inside componentes. Anyway, it doesn't make sence !!!

# Dapr on Kubernetes
`dapr init -k` 

```
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update
helm install redis bitnami/redis 
```

Once redis has been created on Kubernetes, a secret is also create with a redis password. So **secretKeyRef** in the bellow .yaml are: "redis" and "redis-password" by default.
```
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: counter-store
  namespace: default
spec:
  type: state.redis
  version: v1
  metadata:
  - name: redisHost
    value: redis-master.default.svc.cluster.local:6379
  - name: redisPassword    
    secretKeyRef:
      name: redis
      key: redis-password
```

Use this statement to install redis without password (not recommended for Production environments)
```
helm install redis bitnami/redis --set "usePassword=false"
```
So, for this cases, leave password blank, inside above component declaration or inside Kubernetes Secret.