# REF: https://github.com/andreatosato/dapr-cloudchampion/blob/azure-container-apps/deploy/AzureContainerApps/CreateEnv.ps1

$STORAGE_ACCOUNT_KEY="<YOUR-STORAGE-ACCOUNT-KEY>"
$SERVICE_BUS_CONNECTION_STRING="<YOUR-SERVICE-BUS-CONNECTION-STRING>"
$RESOURCE_GROUP="<YOUR-RESOURCE-GROUP>"
$CONTAINERAPPS_ENVIRONMENT="AllTogetherAppEnv"
$ACR="<YOUR-ACR>.azurecr.io"
$ACR_User="<YOUR-ACR-USER>"
$ACR_Password=$(az acr credential show -n $ACR --query "passwords[0].value" -o tsv)
$LOG_ANALYTICS_WORKSPACE="AllTogetherAppWorkspaceLogs"
$LOCATION="northeurope" # <northeurope | canadacentral>  Currenty available just for northeurope and canadacentral"

# az login
# az account set -s 0f64236e-5a90-4d9c-8bd5-c0bc00000000
# az extension add --source https://workerappscliextension.blob.core.windows.net/azure-cli-extension/containerapp-0.2.0-py2.py3-none-any.whl
# az provider register --namespace Microsoft.Web

az monitor log-analytics workspace create --resource-group $RESOURCE_GROUP --workspace-name $LOG_ANALYTICS_WORKSPACE
$LOG_ANALYTICS_WORKSPACE_CLIENT_ID=(az monitor log-analytics workspace show --query customerId -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)
$LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=(az monitor log-analytics workspace get-shared-keys --query primarySharedKey -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)

az containerapp env create `
  --name $CONTAINERAPPS_ENVIRONMENT `
  --resource-group $RESOURCE_GROUP `
  --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID `
  --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET `
  --location "$LOCATION"

az containerapp create `
 --name counterservice `
 --resource-group $RESOURCE_GROUP `
 --environment $CONTAINERAPPS_ENVIRONMENT `
 --secrets "accountkey=$STORAGE_ACCOUNT_KEY,connectionstring=$SERVICE_BUS_CONNECTION_STRING" `
 --registry-login-server $ACR `
 --registry-username $ACR_User `
 --registry-password $ACR_Password `
 --image k8sappsregistry.azurecr.io/alltogether.counterservice:latest `
 --target-port 80 `
 --ingress 'internal' `
 --min-replicas 1 `
 --max-replicas 1 `
 --enable-dapr `
 --dapr-app-port 80 `
 --dapr-app-id counterservice `
 --dapr-components ..\..\dapr\components\azure\components.yaml `
 --verbose

 az containerapp create `
 --name weatherservice `
 --resource-group $RESOURCE_GROUP `
 --environment $CONTAINERAPPS_ENVIRONMENT `
 --secrets "accountkey=$STORAGE_ACCOUNT_KEY,connectionstring=$SERVICE_BUS_CONNECTION_STRING" `
 --registry-login-server $ACR `
 --registry-username $ACR_User `
 --registry-password $ACR_Password `
 --image k8sappsregistry.azurecr.io/alltogether.weatherservice:latest `
 --target-port 80 `
 --ingress 'internal' `
 --min-replicas 1 `
 --max-replicas 1 `
 --enable-dapr `
 --dapr-app-port 80 `
 --dapr-app-id weatherservice `
 --dapr-components ..\..\dapr\components\azure\components.yaml `
 --verbose

 az containerapp create `
 --name website `
 --resource-group $RESOURCE_GROUP `
 --environment $CONTAINERAPPS_ENVIRONMENT `
 --secrets "accountkey=$STORAGE_ACCOUNT_KEY,connectionstring=$SERVICE_BUS_CONNECTION_STRING" `
 --registry-login-server $ACR `
 --registry-username $ACR_User `
 --registry-password $ACR_Password `
 --image k8sappsregistry.azurecr.io/alltogether.website:latest `
 --target-port 80 `
 --ingress 'external' `
 --min-replicas 1 `
 --max-replicas 1 `
 --enable-dapr `
 --dapr-app-port 80 `
 --dapr-app-id website `
 --dapr-components ..\..\dapr\components\azure\components.yaml `
 --verbose