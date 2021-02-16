using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Schuellerrat.InputModels;
using Schuellerrat.Services;

namespace Schuellerrat.Controllers
{
    using Services.EmailSender;

    public class ContactController : Controller
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult ContactForm()
        {
            return this.View("ContactForm");
        }

        [HttpPost]
        public async Task<IActionResult> ContactForm(EmailInputModel inputModel)
        {
            await this.emailSender.SendEmailAsync(inputModel.From, inputModel.FromName, inputModel.To, inputModel.Subject,
                inputModel.HtmlContent);

            return this.Redirect("/Contact/ContactForm");
        }
    }
}
