using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sample.Web.NewsletterSubscription.Interfaces;

namespace Sample.Web.NewsletterSubscription.Pages.News;

public class ViewPageModel : PageModel
{
    private readonly INewsletterService newsletterService;
    private readonly INewsService newsService;
    private readonly ILogger<ViewPageModel> logger;

    public ViewPageModel(INewsletterService newsletterService, INewsService newsService, ILogger<ViewPageModel> logger)
    {
        this.newsletterService = newsletterService;
        this.newsService = newsService;
        this.logger = logger;
    }

    public async Task OnGet()
    {
        logger.LogInformation("News view loaded with query {0}", Query);
        var news = await newsService.SearchNewsAsync(Query);
        logger.LogInformation("Loaded {0} news", news.Count);
        NewsList = news;
    }

    [BindProperty(SupportsGet = true)] public string Query { get; set; }
    [BindProperty] public List<Models.News> NewsList { get; set; }
}