using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Third_Aks_Web_Managed.Interfaces;

namespace Third_Aks_Web_Managed.Pages.Kubek;

public class DashboardPageModel : PageModel
{
    private readonly IKubernetesService kubernetesService;
    private readonly ILogger<DashboardPageModel> logger;

    public DashboardPageModel(IKubernetesService kubernetesService, ILogger<DashboardPageModel> logger)
    {
        this.kubernetesService = kubernetesService;
        this.logger = logger;
    }

    public async Task OnGetAsync()
    {
        logger.LogInformation("Getting cluster name");
        var name = await kubernetesService.GetClusterNameAsync();
        Name = name;
        logger.LogInformation("Cluster name is {0}", name);
    }

    [BindProperty]
    public string Name { get; set; }
}