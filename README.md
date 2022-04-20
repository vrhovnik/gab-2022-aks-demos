# Global Azure Bootcamp 2022 demos

Demos for Global Azure Bootcamp 2022 session [**Azure Kubernetes Service programmatic
management**](https://globalazure.net/sessions/336828).

Azure Kubernetes Service is a managed service offering for working/operating with Kubernetes cluster in Azure. In this
session the focus is on checking out different (most common) tools to control, manage and operate clusters and
workloads in the Azure Kubernetes Service. It goes through service built-in options, use 3rd party tools (Portainer /
Rancher), and focus
on programmatic approach with custom scenarios / your requirements with the use of C#, PowerShell and REST.

## Demo structure

**TBD**

## Setup instructions

In order to run the application, you will need to have [.NET](https://dot.net) installed. I do recommend having fully
pledged IDE (f.e. [Visual Studio](https://www.visualstudio.com), [JetBrains Rider](https://www.jetbrains.com/rider/))
or [Visual Studio Code](https://code.visualstudio.com)
with [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) to check and navigate the
source code.

You will need to have [Azure Subscription](https://azure.microsoft.com/en-us/free/) and a
working [Azure Kubernetes Service](https://azure.microsoft.com/en-us/services/kubernetes-service/#overview) instance.

To work with the command line and to test out functionalities you will need to
have [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest). To ease up the
work with Kubernetes, I do recommend having [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
installed.

As part of the application, I am using [Azure Storage](https://docs.microsoft.com/en-us/azure/storage/) to store
different config
files (for the purposes of this demo). You will need to fill in the details
about [Storage connection string](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=/azure/storage/blobs/toc.json)
and container name for demo to be able to connect to the cluster.

If you want to get remote access to populated container images from a remote docker host (setting **DockerHostUrl**),
you can
follow [this tutorial here](https://docs.docker.com/engine/install/linux-postinstall/#configuring-remote-access-with-daemonjson)
in order to allow TCP connectivity and provide URL (IP) to the application to show image list.

For logging purposes I
use [Application Insight](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview). If you want
to measure performance and see detailed logs (and many more), follow
this [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core). You will need to provide **
Instrumentation key** for app to send data and logs to AI.

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
