using k8s;
using Microsoft.Extensions.Options;
using Third_Aks_Web_Managed.Interfaces;
using Third_Aks_Web_Managed.Options;

namespace Third_Aks_Web_Managed.Services;

public class AksService : IKubernetesService
{
    private readonly IStorageWorker storageWorker;
    private readonly ILogger<AksService> logger;
    private readonly KubekOptions kubekOptions;
        
    public AksService(IOptions<KubekOptions> kubekOptionsValue, 
        IStorageWorker storageWorker, ILogger<AksService> logger)
    {
        this.storageWorker = storageWorker;
        this.logger = logger;
        kubekOptions = kubekOptionsValue.Value;
    }

    public async Task<string> GetClusterNameAsync()
    {
        try
        {
            logger.LogInformation("Getting cluster information - client - GetClusterNameAsync");
            var stream = await storageWorker.DownloadFileAsync(kubekOptions.ConfigFileName);
            var config =  KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            logger.LogInformation("Getting host name from config file");
            return config.Host;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return string.Empty;
    }

    public async Task<Kubernetes?> LoadBasedOnConfigurationAsync()
    {
        try
        {
            var kubeConfigFile = kubekOptions.ConfigFileName;
            logger.LogInformation("Getting config file from Azure Storage {0}", kubeConfigFile);
            var stream = await storageWorker.DownloadFileAsync(kubeConfigFile);
            logger.LogInformation("Getting cluster information - client - LoadBasedOnConfigurationAsync");
            var config =  KubernetesClientConfiguration.BuildConfigFromConfigFile(stream);
            var client = new Kubernetes(config);
            return client;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return null;
    }
}