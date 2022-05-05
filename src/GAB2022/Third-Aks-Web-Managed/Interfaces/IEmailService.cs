namespace Third_Aks_Web_Managed.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string from, string to,string subject, string body);
}