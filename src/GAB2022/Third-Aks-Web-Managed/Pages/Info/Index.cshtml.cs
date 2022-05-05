using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Third_Aks_Web_Managed.Pages.Info;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    public IndexModel(ILogger<IndexModel> logger) => this.logger = logger;

    public void OnGet()
    {
        logger.LogInformation("Index page loaded at {0}", DateTime.Now);
    }
}