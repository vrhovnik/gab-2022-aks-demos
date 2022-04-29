namespace Sample.Web.NewsletterSubscription.Models;

public class News
{
    public string NewsId { get; set; }
    public string NewsletterId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortTitle { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}