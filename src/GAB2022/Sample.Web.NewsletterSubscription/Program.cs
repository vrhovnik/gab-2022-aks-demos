using Sample.Web.NewsletterSubscription.Interfaces;
using Sample.Web.NewsletterSubscription.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<INewsletterService, LocalNewsletterService>();
builder.Services.AddScoped<INewsService, LocalNewsService>();

builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
    options.Conventions.AddPageRoute("/Info/Index", ""));

var app = builder.Build();

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();