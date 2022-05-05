using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Third_Aks_Web_Managed.Interfaces;
using Third_Aks_Web_Managed.Options;
using Third_Aks_Web_Managed.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
//options
builder.Services.Configure<AzureAdOptions>(builder.Configuration.GetSection("AzureAd"));
builder.Services.Configure<StorageOptions>(builder.Configuration.GetSection("StorageOptions"));
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGridOptions"));
builder.Services.Configure<KubekOptions>(builder.Configuration.GetSection("KubekOptions"));

//core services
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationInsightsTelemetry();

//authentication support with Azure AD
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration);
builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

//send grid options
var sendGridSettings = builder.Configuration.GetSection("SendGridOptions").Get<SendGridOptions>();
builder.Services.AddScoped<IEmailService, SendGridEmailSender>(
    _ => new SendGridEmailSender(sendGridSettings.ApiKey));

var app = builder.Build();
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
    options.Conventions.AddPageRoute("/Info/Index", ""));

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();