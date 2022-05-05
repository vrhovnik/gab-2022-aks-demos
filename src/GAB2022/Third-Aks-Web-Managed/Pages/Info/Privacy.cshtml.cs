using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Third_Aks_Web_Managed.Pages.Info;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> logger;

    public PrivacyModel(ILogger<PrivacyModel> logger) => this.logger = logger;

    public void OnGet() => logger.LogInformation("Privacy page was loaded at {0}", DateTime.Now);
}