using System.Threading.Tasks;

namespace OngProject.Core.Business.Mail.Interfaces
{
    public interface IMailBusiness
    {
        Task SendEmailAsync(string toEmail);
    }
    
}

