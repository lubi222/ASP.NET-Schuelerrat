using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schuellerrat.Services.Email
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
