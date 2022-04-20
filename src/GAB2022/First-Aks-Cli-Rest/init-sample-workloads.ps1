# check 0-Demo-Initial/0-aks-basic-setup-script.ps1 for pre-requisites

# create namespace via kubectl
kubectl create namespace rest

# create few pods to play around with REST
kubectl run rest-demo-1 --image=<acr-name>.azurecr.io/hello-world:latest --port=80 --namespace=rest
kubectl run rest-demo-2 --image=<acr-name>.azurecr.io/hello-world:latest --port=80 --namespace=rest

# delete namespace test
kubectl delete namespace rest
