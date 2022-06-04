using System.Threading.Tasks;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Repositories.Mail.Interfaces;

namespace OngProject.Core.Business.Mail
{
    public class MailBusiness : IMailBusiness
    {
        private IMailRepository _mailRepository;
        
        public MailBusiness(IMailRepository mailRepository)
        {
            this._mailRepository = mailRepository;
        }
        public async Task SendEmailAsync(string toEmail)
        {
            await _mailRepository.SendEmailAsync(toEmail);
        }
    }

}

