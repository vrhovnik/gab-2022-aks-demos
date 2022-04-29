using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Interfaces;

public interface INewsService
{
    Task<List<News>> SearchNewsAsync(string query, int page = 10);
    Task<News> GetNewsDetailsAsync(string newsId);
}