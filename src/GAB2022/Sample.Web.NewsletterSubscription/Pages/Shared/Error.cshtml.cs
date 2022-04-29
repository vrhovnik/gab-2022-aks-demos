using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sample.Web.NewsletterSubscription.Pages.Shared;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> logger;

    public ErrorModel(ILogger<ErrorModel> logger) => this.logger = logger;

    public void OnGet()
    {
        logger.LogInformation("Error paga has been loaded - with trace {0}", HttpContext.TraceIdentifier);
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}