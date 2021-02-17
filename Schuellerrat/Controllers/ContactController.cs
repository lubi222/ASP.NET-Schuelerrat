﻿using System;
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
    using Services.EmailSender;

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
                Subject = $"{inputModel.Name} - {inputModel.Grade}th Grade",
                Body = inputModel.Content,
            };

            await mailService.SendEmailAsync(request);
            return this.Redirect("/Contact/ContactForm");
        }


    }
}
