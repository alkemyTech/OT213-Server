using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OngProject.Repositories.Mail.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OngProject.Repositories.Mail
{
    public class MailRepository : IMailRepository
    {
        private IConfiguration _configuration;
        public MailRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail)
        {
            var path = "Templates/welcome.html";

            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("guillermo.donalisio1@gmail.com", "Welcome!");
            var subject = "Welcome to Alkemy";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "Mail sent by SendGrid with C#";
            var htmlContent = File.ReadAllText(path);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }

}

