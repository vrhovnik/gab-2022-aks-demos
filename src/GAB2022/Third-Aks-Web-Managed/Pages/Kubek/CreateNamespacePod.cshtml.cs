using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Third_Aks_Web_Managed.Helpers;
using Third_Aks_Web_Managed.Interfaces;
using Third_Aks_Web_Managed.Models;

namespace Third_Aks_Web_Managed.Pages.Kubek;

public class CreateNamespacePodPageModel : PageModel
{
    private readonly IContainerRegistryService containerRegistryService;
    private readonly IKubernetesCrud kubernetesCrud;
    private readonly ILogger<CreateNamespacePodPageModel> logger;

    public CreateNamespacePodPageModel(IContainerRegistryService containerRegistryService,
        IKubernetesCrud kubernetesCrud,
        ILogger<CreateNamespacePodPageModel> logger)
    {
        this.containerRegistryService = containerRegistryService;
        this.kubernetesCrud = kubernetesCrud;
        this.logger = logger;
    }

    [BindProperty] public string NamespaceName { get; set; }
    [BindProperty] public string PodName { get; set; }
    [TempData] public string InfoText { get; set; }
    [BindProperty] public List<DockerImageViewModel> Images { get; set; }

    public void OnGet()
    {
        logger.LogInformation("Loading containers");
        var list = containerRegistryService.GetPredefinedImages();
        logger.LogInformation("Loaded list of {0} images", list.Count);
        Images = list;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(NamespaceName))
        {
            InfoText = "Enter name";
            return RedirectToPage("/Kubek/CreateNamespacePod");
        }

        if (!await kubernetesCrud.CreateNamespaceAsync(NamespaceName))
        {
            InfoText = "There has been an error with creating namespace in k8s, try again";
            return RedirectToPage("/Kubek/CreateNamespacePod");
        }

        var form = await Request.ReadFormAsync();
        var imageName = form["image"];
        logger.LogInformation($"Received {imageName}");
        if (!string.IsNullOrEmpty(imageName))
        {
            if (string.IsNullOrEmpty(PodName)) PodName = Utils.GenerateName(5).ToLower();

            if (!await kubernetesCrud.CreatePodAsync(NamespaceName, PodName, imageName))
            {
                InfoText = "There has been an error in creating pod, try again";
                return RedirectToPage("/Kubek/CreateNamespacePod");
            }
        }

        logger.LogInformation("Pod, namespace created!");
        return RedirectToPage("/Kubek/ListNamespaces");
    }
}