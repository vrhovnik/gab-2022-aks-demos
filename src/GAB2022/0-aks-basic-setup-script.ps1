## Execute script line by line to have azure aks ready

# get subscriptions and set the right one
az account list -o table
az account set --subscription "<subscription-id>"

# create resource group
az group create --name "<resource-group-name>" --location "<location>"

# create AKS cluster with previously created resource group
az aks create --resource-group "<resource-group-name>" --name "<cluster-name>" --node-count 2 --generate-ssh-keys

# you can also use AKS deployment helper with basics how to create cluster
# More here: 
Start-Process "https://azure.github.io/AKS-Construction/"

# get AKS cluster list
az aks list -o table

# get AKS nodes to see if it is ready
az aks nodepool list -g $RESOURCE_GROUP -n $AKS_CLUSTER_NAME -o table

# install tools for working with cluster
az aks install-cli 

# get AKS cluster credentials to be working with that cluster
az aks get-credentials -g $RESOURCE_GROUP -n $AKS_CLUSTER_NAME

## CLEAN UP
# delete resource group and cluster
# az group delete -n MyResourceGroup --yes
 