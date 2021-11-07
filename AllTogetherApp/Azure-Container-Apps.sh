az containerapp create \
  --name counterservice \
  --resource-group k8s-Apps \
  --environment AllTogetherApp \
  --image k8sappsregistry.azurecr.io/alltogether.counterservice:latest \
  --target-port 443 \
  --ingress 'internal' \
  --min-replicas 1 \
  --max-replicas 1 \
  --enable-dapr \
  --dapr-app-port 5002 \
  --dapr-app-id counterservice \
  --dapr-components (./dapr/components/azure/pubsub.yaml, ./dapr/components/azure/state.yaml, ./dapr/components/azure/secrets.yaml)

  az containerapp create \
  --name weatherservice \
  --resource-group k8s-Apps \
  --environment AllTogetherApp \
  --image k8sappsregistry.azurecr.io/alltogether.weatherservice:latest \
  --target-port 443 \
  --ingress 'internal' \
  --min-replicas 1 \
  --max-replicas 1 \
  --enable-dapr \
  --dapr-app-port 5003 \
  --dapr-app-id weatherservice \
  --dapr-components ./dapr/components/azure

  az containerapp create \
  --name website \
  --resource-group k8s-Apps \
  --environment AllTogetherApp \
  --image k8sappsregistry.azurecr.io/alltogether.website:latest \
  --target-port 443 \
  --ingress 'external' \
  --min-replicas 1 \
  --max-replicas 1 \
  --enable-dapr \
  --dapr-app-port 5000 \
  --dapr-app-id website \
  --dapr-components ./dapr/components/azure