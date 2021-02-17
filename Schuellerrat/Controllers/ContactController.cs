using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Schuellerrat.InputModels;
using Schuellerrat.Services;
using Schuellerrat.Services.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Schuellerrat.Controllers
{

    public class ContactController : Controller
    {
        private readonly IMailService mailService;

        public ContactController(IMailService mailService)
        {
            this.mailService = mailService;
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

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var request = new MailRequest()
            {
                ToEmail = "elidimitrova036@gmail.com",
                Subject = CreateSubject(inputModel.Name, inputModel.Grade),
                Body = inputModel.Content + "\n Имейл за обратна връзка:" + inputModel.Email,
            };

            await mailService.SendEmailAsync(request);
            return this.Redirect("/Contact/ContactForm");
        }

        private string CreateSubject(string name, int? grade)
        {
            if (name == null && grade == null)
            {
                return "Anonymous";
            }
            else if (name == null)
            {
                return $"Anonymous - {grade}th Grade";
            }
            else if (grade == null)
            {
                return $"{name}";
            }
            else
            {
                return $"{name} - {grade}th Grade";
            }
        }
    }
}