using Microsoft.Azure.Management.ContainerRegistry.Fluent;
using Third_Aks_Web_Managed.Models;

namespace Third_Aks_Web_Managed.Interfaces;

public interface IContainerRegistryService
{
    Task<IRegistry> GetRegistryRepositoriesAsync(string containerRegistryName);
    Task<List<DockerImageViewModel>> GetImagesForRepositoryAsync(string containerRegistryName);
    List<DockerImageViewModel> GetPredefinedImages();
}