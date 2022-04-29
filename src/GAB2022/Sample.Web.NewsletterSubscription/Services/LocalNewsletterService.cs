using Sample.Web.NewsletterSubscription.Interfaces;
using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Services;

public class LocalNewsletterService : INewsletterService
{
    private readonly List<Newsletter> list = new()
    {
        new Newsletter
        {
            NewsletterId = "Newsletter-1",
            Title = "Azure",
            Destination = "https://azurecomcdn.azureedge.net/en-us/blog/feed/"
        },
        new Newsletter
        {
            NewsletterId = "Newsletter-2",
            Title = "Beyondlocalhost",
            Destination = "https://beyondlocalhost.tech/feed/"
        },
        new Newsletter
        {
            NewsletterId = "Newsletter-3",
            Title = ".NET blog",
            Destination = "https://devblogs.microsoft.com/dotnet/feed/"
        }
    };

    public async Task<List<Newsletter>> GetNewslettersAsync() => await Task.FromResult(list);

    public async Task<bool> SubscribeAsync(string newsetterId)
    {
        return true;
    }

    public async Task<bool> UnSubscribeAsync(string newsetterId)
    {
        throw new NotImplementedException();
    }
}