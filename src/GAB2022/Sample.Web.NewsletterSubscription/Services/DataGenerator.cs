using Sample.Web.NewsletterSubscription.Models;

namespace Sample.Web.NewsletterSubscription.Services;

public static class DataGenerator
{
    public static List<News> GenerateRandomNews(int number, params Newsletter[] newsletters)
    {
        var list = new List<News>();
        foreach (var newsletter in newsletters)
        {
            int rand = new Random().Next(5, number);
            list.Add(new News()
            {
                NewsletterId = newsletter.NewsletterId,
                DateCreated = DateTime.Now.AddHours(-rand),
                Title = RandomTextGenerator(rand),
                Content = RandomTextGenerator(rand + 20),
                Description = RandomTextGenerator(rand + 5),
                ShortTitle = RandomTextGenerator(rand - 2),
                NewsId = Guid.NewGuid().ToString()
            });
        }

        return list;
    }

    private static string RandomTextGenerator(int size = 10)
    {
        var res = new Random();

        string str = "abcdefghijklmnopqrstuvwxyz";
        var ran = "";
        for (var i = 0; i < size; i++)
        {
            var x = res.Next(26);
            ran += str[x];
        }

        return ran;
    }
}