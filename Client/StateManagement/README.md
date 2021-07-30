# Dapr .NET SDK state management example

## Prerequisites

- [.NET Core 3.1 or .NET 5+](https://dotnet.microsoft.com/download) installed
- [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
- [Initialized Dapr environment](https://docs.dapr.io/getting-started/install-dapr-selfhost/)
- [Dapr .NET SDK](https://docs.dapr.io/developing-applications/sdks/dotnet/)

## Running the example

To run the sample locally run this command in the DaprClient directory:

```sh
dapr run --app-id DaprClient -- dotnet run <sample number>
```

Running the following command will output a list of the samples included:

```sh
dapr run --app-id DaprClient -- dotnet run
```

Press Ctrl+C to exit, and then run the command again and provide a sample number to run the samples.

For example run this command to run the 0th sample from the list produced earlier.

```sh
dapr run --app-id DaprClient -- dotnet run 0
```

## State operations

See [StateStoreExample.cs](./StateStoreExample.cs) for an example of using `DaprClient` for basic state store operations like get, set, and delete.

# State transactions

See: [StateStoreTransactionsExample.cs](./StateStoreTransactionsExample.cs) for an example of using `DaprClient` for transactional state store operations that affect multiple keys. 

## ETags

See [StateStoreETagsExample.cs](./StateStoreETagsExample.cs) for an example of using `DaprClient` for optimistic concurrency control with the state store.


# Working with Azure (Key Vault)
1. Create KeyVault, like: "dapr-keyvault"
2. Create ServicePrincipalName (spn) with certificate in keyvault:
`az ad sp create-for-rbac --name dapr-ServicePrincipalName --create-cert --cert dapr-CertName --keyvault dapr-keyvault`

Ouput Sample:
```
{
  "appId": "b8ec9999-d99d-999d-9999-333b3333a33a",
  "displayName": "dapr-ServicePrincipalName",
  "name": "b8ec9999-d99d-999d-9999-333b3333a33a",
  "password": null,
  "tenant": "38f99999-9c99-99d9-9c9c-f9999b9a9999"
}
```
**Note:** How to get appId: `az ad sp list --display-name ServicePrincipal`

3. Access to KeyVault (via rbac) to the SP (ServicePrincipal) once created in the previous step.

4. Download the certificate (.pfx). Also from command line:
`az keyvault secret download --vault-name dapr-keyvault --name CertName --encoding base64 --file dapr-keyvault.pfx`

**Note:** If an error found like this: *'StatusCode="FailedPrecondition", Detail="secret store is not configured'*, it could be because an error in the .yaml format, so it's time to review/check it.

**Important:** Review "components" folder to know Dapr components format 

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
  name: statestore
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
