using Sample.Web.NewsletterSubscription.Interfaces;
using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Services;

public class LocalNewsService : INewsService
{
    private readonly INewsletterService newsletterService;

    public LocalNewsService(INewsletterService newsletterService)
    {
        this.newsletterService = newsletterService;
    }

    public async Task<List<News>> SearchNewsAsync(string query, int page = 10)
    {
        var newsletters = await newsletterService.GetNewslettersAsync();
        var randomNews = DataGenerator.GenerateRandomNews(10, newsletters.ToArray());
        return string.IsNullOrEmpty(query)
            ? randomNews
            : randomNews.Where(d => d.Title.Contains(query) || d.Content.Contains(query)).ToList();
    }

    public async Task<News> GetNewsDetailsAsync(string newsId)
    {
        throw new NotImplementedException();
    }
}