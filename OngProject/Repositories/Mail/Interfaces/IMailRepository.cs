using System.Threading.Tasks;

namespace OngProject.Repositories.Mail.Interfaces
{
    public interface IMailRepository
    {
        Task SendEmailAsync(string toEmail);
    }
}

