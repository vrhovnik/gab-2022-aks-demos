using k8s;
using k8s.Models;
using Third_Aks_Web_Managed.Interfaces;

namespace Third_Aks_Web_Managed.Services;

public class AKSObjectsService : IKubernetesObjects
{
    private readonly IKubernetesService client;
    private readonly ILogger<AKSObjectsService> logger;

    public AKSObjectsService(IKubernetesService client, ILogger<AKSObjectsService> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public async Task<IEnumerable<V1Namespace>> ListNamespacesAsync()
    {
        logger.LogInformation("Getting cluster information - client - ListNamespacesAsync");
        var kubernetes = await client.LoadBasedOnConfigurationAsync();
        logger.LogInformation("Listining namespaces");
        try
        {
            var list = await kubernetes.ListNamespaceAsync();
            return list.Items;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
        return new List<V1Namespace>();
    }

    public async Task<IEnumerable<V1Pod>> ListPodsAsync(string namespaceName = "default")
    {
        logger.LogInformation("Getting cluster information - client - ListPodsAsync");
        var kubernetes = await client.LoadBasedOnConfigurationAsync();
        logger.LogInformation("Listining pods");
        try
        {
            var list = await kubernetes.ListNamespacedPodAsync(namespaceName);
            return list.Items;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
        return new List<V1Pod>();
    }
}