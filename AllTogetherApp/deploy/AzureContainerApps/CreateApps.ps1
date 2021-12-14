# REF: https://github.com/andreatosato/dapr-cloudchampion/blob/azure-container-apps/deploy/AzureContainerApps/CreateEnv.ps1


$TENANT_ID=""
$CLIENT_ID=""
$CERTIFICATE=""
$STORAGE_ACCOUNT_KEY=""

# az login
# az account set -s 0f64236e-5a90-4d9c-8bd5-c0bc00000000

az containerapp create `
 --name counterservice `
 --resource-group k8s-Apps `
 --environment AllTogetherApp `
 --secrets "tenantid=$TENANT_ID,clientid=$CLIENT_ID,certificate=$CERTIFICATE,storageaccountkey=$STORAGE_ACCOUNT_KEY" `
 --image k8sappsregistry.azurecr.io/alltogether.counterservice:latest `
 --target-port 443 `
 --ingress 'internal' `
 --min-replicas 1 `
 --max-replicas 1 `
 --enable-dapr `
 --dapr-app-port 5002 `
 --dapr-app-id counterservice `
 --dapr-components ..\..\dapr\components\azure\components.yaml `
 --verbose

