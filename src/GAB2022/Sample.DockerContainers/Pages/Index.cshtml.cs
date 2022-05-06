using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.BasicAuth;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Management.ContainerRegistry.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.DockerContainers.Models;
using Third_Aks_Web_Managed.Options;

namespace Sample.DockerContainers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IOptions<AzureAdOptions> azureAdOptionsValue;
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IOptions<AzureAdOptions> azureAdOptionsValue, ILogger<IndexModel> logger)
        {
            this.azureAdOptionsValue = azureAdOptionsValue;
            this.logger = logger;
        }

        public async Task OnGetAsync()
        {
            logger.LogInformation("Getting info from configuration and logging into Azure");
            var azureAdOptions = azureAdOptionsValue.Value;

            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(azureAdOptions.ClientId, azureAdOptions.ClientSecret,
                    azureAdOptions.TenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Azure.Configure().Authenticate(credentials)
                .WithSubscription(azureAdOptions.SubscriptionId);
            
            logger.LogInformation("Logged into Azure");
            
            var registry =
                await azure.ContainerRegistries.GetByResourceGroupAsync(azureAdOptions.AcrRG,
                    "acontainers");
            
            var list = new List<DockerImageViewModel>();
            logger.LogInformation("Getting docker client to call VM to get images");

            try
            {
                var credRegistry = await registry.GetCredentialsAsync();
                //appropriate RBAC needs to be 
                var dockerCredentials =
                    new BasicAuthCredentials(credRegistry.Username, 
                        credRegistry.AccessKeys[AccessKeyType.Primary]);

                using var client = new DockerClientConfiguration(new Uri(registry.LoginServerUrl), dockerCredentials)
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
        }

        [BindProperty]
        public List<DockerImageViewModel> DockerImages { get; set; }
    }
}