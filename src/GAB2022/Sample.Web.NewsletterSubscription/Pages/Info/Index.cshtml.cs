using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sample.Web.NewsletterSubscription.Interfaces;
using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Pages.Info;

public class IndexModel : PageModel
{
    private readonly INewsletterService newsletterService;
    private readonly ILogger<IndexModel> logger;

    public IndexModel(INewsletterService newsletterService, ILogger<IndexModel> logger)
    {
        this.newsletterService = newsletterService;
        this.logger = logger;
    }

    public async Task OnGetAsync()
    {
        logger.LogInformation("Info page loaded");
        var newsletters = await newsletterService.GetNewslettersAsync();
        logger.LogInformation("Loaded {0} newsletters", newsletters.Count);
        Newsletters = newsletters;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var formCollection = await Request.ReadFormAsync();
        var newsletterSelection = formCollection["ddlNewsletters"];
        var message = "Subscription was not successful";
        if (!string.IsNullOrEmpty(newsletterSelection))
        {
            var newsetterId = newsletterSelection[0];
            await newsletterService.SubscribeAsync(newsetterId);
            message = $"Subscribed to newsletter with ID: {newsetterId}";
            logger.LogInformation("Subscribed to {0}", newsetterId);
        }

        TempData["MessageInfo"] = message;
        return RedirectToAction("Index");
    }

    [BindProperty] public List<Newsletter> Newsletters { get; set; }
}