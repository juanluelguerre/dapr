# Binding sample

In this sample we can see how Dapr works using binding:
1. Create an Storage account and after that a queue must be also created.
2. Create Dapr component:
```
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: bindingeventdemo1
  namespace: default
spec:
  type: bindings.azure.storagequeues
  version: v1
  metadata:
  - name: storageAccount
    value: "<AZURE-STORAGE-ACCOUNT-NAMNE>"
  - name: storageAccessKey
    value: "<AZURE-STORAGE-ACCOUNT-KEY>"
  - name: queue
    value: "<QUEUE-NAME>"
  - name: ttlInSeconds
    value: "60"
```
3. Add a Post method (with no name)) inside API Controller and be sure api name (*controller name*) has the same name as the Dapr component (*metadata.name*).
4. Execute Dapr runtime:
```
dapr run --app-id bindingeventdemo1 `
    --app-port 5000 `
    --dapr-http-port 3500 `
    --dapr-grpc-port 50000 `
    --components-path ./dapr/components `
    dotnet run -p ./ApiApp/ApiApp.csproj
```