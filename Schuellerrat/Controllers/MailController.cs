using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schuellerrat.InputModels;
using Schuellerrat.Services.Email;

namespace Schuellerrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] EmailInputModel inputModel)
        {
            try
            {
                var request = new MailRequest()
                {
                    ToEmail = "yLiubomir@gmail.com",
                    Subject = inputModel.Name + inputModel.Grade == null ? "" : inputModel.Grade.ToString(),
                    Body = inputModel.Content,
                };
                
                await mailService.SendEmailAsync(request);
                return this.Redirect("/Contact/ContactForm");
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
