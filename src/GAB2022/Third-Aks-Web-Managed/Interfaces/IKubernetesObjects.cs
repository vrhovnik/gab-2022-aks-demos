using k8s.Models;

namespace Third_Aks_Web_Managed.Interfaces;

public interface IKubernetesObjects
{
    Task<IEnumerable<V1Namespace>> ListNamespacesAsync();
    Task<IEnumerable<V1Pod>> ListPodsAsync(string namespaceName = "default");
}