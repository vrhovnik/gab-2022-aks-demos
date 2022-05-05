using k8s;

namespace Third_Aks_Web_Managed.Interfaces;

public interface IKubernetesService
{
    Task<string> GetClusterNameAsync();
    Task<Kubernetes?> LoadBasedOnConfigurationAsync();
}