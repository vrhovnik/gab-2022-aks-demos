using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Interfaces;

public interface INewsletterService
{
    Task<List<Newsletter>> GetNewslettersAsync();
    Task<bool> SubscribeAsync(string newsetterId);
    Task<bool> UnSubscribeAsync(string newsetterId);
}