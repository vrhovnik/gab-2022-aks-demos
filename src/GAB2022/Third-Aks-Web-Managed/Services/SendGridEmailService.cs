using System.Diagnostics;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using Third_Aks_Web_Managed.Interfaces;

namespace Third_Aks_Web_Managed.Services;

public class SendGridEmailSender : IEmailService
{
    private readonly SendGridClient sendGridClient;

    public SendGridEmailSender(string key) => sendGridClient = new SendGridClient(key);

    public async Task<bool> SendEmailAsync(string from, string to, string subject, string body)
    {
        var msg = new SendGridMessage();

        msg.SetFrom(new EmailAddress(from, from));

        var recipients = new List<EmailAddress> {new EmailAddress(to, to)};

        msg.AddTos(recipients);

        msg.SetSubject(subject);
        msg.AddContent(MimeType.Html,body);

        try
        {
            var response = await sendGridClient.SendEmailAsync(msg);
            return response.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return false;
        }
    }
}