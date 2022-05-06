# Global Azure Bootcamp 2022 demos

Demos for Global Azure Bootcamp 2022 session [**Azure Kubernetes Service programmatic
management**](https://globalazure.net/sessions/336828).

Azure Kubernetes Service is a managed service offering for working/operating with Kubernetes cluster in Azure. In this
session the focus is on checking out different (most common) tools to control, manage and operate clusters and
workloads in the Azure Kubernetes Service. It goes through service built-in options, use 3rd party tools (Portainer /
Rancher), and focus
on programmatic approach with custom scenarios / your requirements with the use of C#, PowerShell and REST.

## Demo structure

![solution view](https://webeudatastorage.blob.core.windows.net/web/gab-2022-demo-structure.png)

Demo contains 2 main parts:
1. **0-Demo-Initial**, which contains basic files to get started with the demo (settings up cluster, Azure Container Registry creation and registration, etc.)
2. **Demos** sections, which contains the actual demo code:

### First-Aks-Cli-Rest 

It needs [bootstrap token](https://kubernetes.io/docs/reference/access-authn-authz/bootstrap-tokens/) to authenticate against API. You can use [Postman](https://www.postman.com/) or [curl](https://en.wikipedia.org/wiki/CURL) in order to issue the command. To successfully run this project, you will need to provide **BearerToken** and **ClusterBaseAddress** as [environment variables](https://en.wikipedia.org/wiki/Environment_variable).

The easiest way is to create [service account](https://kubernetes.io/docs/reference/access-authn-authz/service-accounts-admin/) and then do role binding on a cluster to define access levels. With that defined, you can then query the secret to get the token. Use this command `kubectl -n kube-system describe secret $(kubectl -n kube-system get secret | grep youraccountname | awk '{print $1}')`.

### Second-Aks-Cli-Managed

It needs [kubeconfig file]((https://kubernetes.io/docs/concepts/configuration/organize-cluster-access-kubeconfig)) in order to run the application. On Linux check **.kube** hidden folder in home folder (on by default).

If you have [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/) installed, use `kubectl config view` to see the file.

![config view](https://webeudatastorage.blob.core.windows.net/web/meetup-config-view.png)

Solution will automatically load the default config file and authenticate against Kubernetes cluster.

### Third-Aks-Web-Managed 

It uses [Azure AD authentication](https://azure.com/sdk) via managed library (C#) to authenticate with AAD to access [Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/aks/). I am using [Microsoft Identity Web authentication library](https://docs.microsoft.com/en-us/azure/active-directory/develop/microsoft-identity-web). [Flow](https://docs.microsoft.com/en-us/azure/active-directory/develop/app-sign-in-flow) is explained here.

To do it stepy by step,[follow](https://docs.microsoft.com/en-us/azure/active-directory/develop/app-objects-and-service-principals) this tutorial. When you have the service principal, you will need to fill in the [following details](https://github.com/bovrhovn/meetup-demo-kubectl-differently/blob/main/src/KubectlSLN/Kubectl.Web/appsettings.json) in configuration setting (or add environment variables):

![settings](https://webeudatastorage.blob.core.windows.net/web/meetup-web-settings.png)

You can find the data in service principal details (created earlier) and Azure AD portal details page. As part of the application, I am using [Azure Storage](https://docs.microsoft.com/en-us/azure/storage/) to store different config files (in demo only one), you will need to fill in the details about [Storage connection string](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=/azure/storage/blobs/toc.json) and container name.

If you want to get remote access to populated container images from a remote docker host (setting **DockerHostUrl**), you can follow [this tutorial here](https://docs.docker.com/engine/install/linux-postinstall/#configuring-remote-access-with-daemonjson) in order to allow TCP connectivity and provide URL (IP) to the application to show image list.

For logging purposes I use [Application Insight](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview). If you want to measure performance and see detailed logs (and many more), follow this [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core). You will need to provide **Instrumentation key** for app to send data and logs to AI.

## Setup instructions

In order to run the application, you will need to have [.NET](https://dot.net) installed. I do recommend having fully
pledged IDE (f.e. [Visual Studio](https://www.visualstudio.com), [JetBrains Rider](https://www.jetbrains.com/rider/))
or [Visual Studio Code](https://code.visualstudio.com)
with [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) to check and navigate the
source code.

You will need to have [Azure Subscription](https://azure.microsoft.com/en-us/free/) and a
working [Azure Kubernetes Service](https://azure.microsoft.com/en-us/services/kubernetes-service/#overview) instance.
You can use the [AKS Deployment Helper](https://azure.github.io/AKS-Construction/) or follow the
script [here](./src/GAB2022/0-aks-basic-setup-script.ps1).

To work with the command line and to test out functionalities you will need to
have [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest). To ease up the
work with Kubernetes, I do recommend having [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
installed.

You can also connect to the cluster
using [Grafana](https://docs.microsoft.com/en-us/azure/azure-monitor/visualize/grafana-plugin) to create visual
appealing dashboards.

# Links

Useful links to read about:

1. [Global Azure Bootcamp](https://globalazure.net/)
2. [AKS Docs](https://docs.microsoft.com/en-us/azure/aks)
3. [AKS Baseline from Microsoft Patterns & Practices team](https://github.com/mspnp/aks-baseline)
4. [AKS Deployment Helper](https://azure.github.io/AKS-Construction/)
5. [AKS Landing Zone Accelerator](https://github.com/Azure/AKS-Landing-Zone-Accelerator)
6. [Kubernetes Api Client Libraries](https://github.com/kubernetes-client)
   and [3rd party community-maintained client libraries](https://kubernetes.io/docs/reference/using-api/client-libraries/#community-maintained-client-libraries)
7. [Kubernetes Api Overview](https://kubernetes.io/docs/reference/using-api/)
   and [controlling access to cluster](https://kubernetes.io/docs/concepts/security/controlling-access/)
8. [Kubeconfig view](https://kubernetes.io/docs/concepts/configuration/organize-cluster-access-kubeconfig/)
9. [Setup kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
10. [Power tools for kubectl](https://github.com/ahmetb/kubectx)
11. [Portainer](https://www.portainer.io/installation/)
12. [Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/aks/)
13. [Application Insight](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)

# Credits

In this demo, we used the following 3rd party libraries and solutions:

1. [Spectre Console](https://github.com/spectresystems/spectre.console/)
2. [C# managed library for Kubernetes](https://github.com/kubernetes-client/csharp)
3. [Portainer](https://www.portainer.io/installation/)
3. [Rancher](https://rancher.com/)

# QUESTIONS / COMMENTS

If you have any questions, comments, open an issue and happy to answer.
