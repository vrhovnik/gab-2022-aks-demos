using Docker.DotNet;
using Docker.DotNet.BasicAuth;
using Docker.DotNet.Models;
using Microsoft.Azure.Management.ContainerRegistry.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Options;
using Third_Aks_Web_Managed.Interfaces;
using Third_Aks_Web_Managed.Models;
using Third_Aks_Web_Managed.Options;

namespace Third_Aks_Web_Managed.Services;

public class ACRService : IContainerRegistryService
{
    private readonly IAzure azure;
    private readonly AzureAdOptions azureAdOptions;
    private readonly ILogger<ACRService> logger;

    public ACRService(IOptions<AzureAdOptions> azureAdOptionsValue, ILogger<ACRService> logger)
    {
        this.logger = logger;
        azureAdOptions = azureAdOptionsValue.Value;

        var credentials = SdkContext.AzureCredentialsFactory
            .FromServicePrincipal(azureAdOptions.ClientId, azureAdOptions.ClientSecret,
                azureAdOptions.TenantId, AzureEnvironment.AzureGlobalCloud);

        azure = Microsoft.Azure.Management.Fluent.Azure
            .Configure()
            .Authenticate(credentials)
            .WithSubscription(azureAdOptions.SubscriptionId);
    }

    public async Task<IRegistry> GetRegistryRepositoriesAsync(string containerRegistryName)
    {
        logger.LogInformation("Retrieving info about registry");
        var registry =
            await azure.ContainerRegistries.GetByResourceGroupAsync(azureAdOptions.AcrRG,
                containerRegistryName);
        logger.LogInformation("Registry info retrieved!");
        return registry;
    }

    public async Task<List<DockerImageViewModel>> GetImagesForRepositoryAsync(string containerRegistryName)
    {
        var list = new List<DockerImageViewModel>();
        logger.LogInformation("Getting docker client to call VM to get images");

        try
        {
            var registry = await GetRegistryRepositoriesAsync(containerRegistryName);
            var credRegistry = await registry.GetCredentialsAsync();

            var credentials =
                new BasicAuthCredentials(credRegistry.Username, credRegistry.AccessKeys[AccessKeyType.Primary]);

            using var client = new DockerClientConfiguration(new Uri(registry.LoginServerUrl), credentials)
                .CreateClient();

            var listImages = await client.Images.ListImagesAsync(
                new ImagesListParameters { All = true });

            logger.LogInformation("Retrieved client, doing list images");
            foreach (var img in listImages)
            {
                if (img.RepoTags is not { Count: > 0 }) continue;
                var name = img.RepoTags[0];
                if (!name.Contains("none"))
                    list.Add(new DockerImageViewModel
                    {
                        Id = img.ID,
                        Name = name
                    });
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        logger.LogInformation("Listening images done");
        return list;
    }

    public List<DockerImageViewModel> GetPredefinedImages()
    {
        var list = new List<DockerImageViewModel>
        {
            new()
            {
                Id = "acontainers.azurecr.io/ew/web:v1",
                Name = "acontainers.azurecr.io/ew/web:v1"
            },
            new()
            {
                Id = "acontainers.azurecr.io/ew/web:v2",
                Name = "acontainers.azurecr.io/ew/web:v2"
            },
            new()
            {
                Id = "acontainers.azurecr.io/ew/status:v2",
                Name = "acontainers.azurecr.io/ew/status:v2"
            },
            new()
            {
                Id = "acontainers.azurecr.io/ew/report:v1",
                Name = "acontainers.azurecr.io/ew/report:v1"
            },
            new()
            {
                Id = "ubuntu:latest",
                Name = "ubuntu:latest"
            }
        };
        return list;
    }
}