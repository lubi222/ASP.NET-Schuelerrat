namespace Schuellerrat.Services.EmailService.Email
{
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
