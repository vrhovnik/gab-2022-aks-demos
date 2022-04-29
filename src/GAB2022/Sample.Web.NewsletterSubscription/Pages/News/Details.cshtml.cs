using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sample.Web.NewsletterSubscription.Interfaces;

namespace Sample.Web.NewsletterSubscription.Pages.News;

public class DetailsPageModel : PageModel
{
    private readonly INewsService newsService;
    private readonly ILogger<DetailsPageModel> logger;

    public DetailsPageModel(INewsService newsService, ILogger<DetailsPageModel> logger)
    {
        this.newsService = newsService;
        this.logger = logger;
    }

    public async Task OnGetAsync(string newsId)
    {
        logger.LogInformation("Loading news for {0}", newsId);
        var news = await newsService.GetNewsDetailsAsync(newsId);
        logger.LogInformation("Loaded {0}", news.Title);
    }

    [BindProperty]
    public Models.News CurrentNews { get; set; }
}