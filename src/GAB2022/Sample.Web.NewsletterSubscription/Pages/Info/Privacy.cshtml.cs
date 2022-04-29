using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sample.Web.NewsletterSubscription.Pages.Info;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> logger;

    public PrivacyModel(ILogger<PrivacyModel> logger) => this.logger = logger;

    public void OnGet() => logger.LogInformation("Privacy page was visited at {0}", DateTime.Now);
}