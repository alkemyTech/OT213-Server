using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Auth.Interfaces
{
    public interface IAuthBusiness
    {
        Task<User> Registrar(User user, string pass);
        Task<bool> ExistsUser(string email);
        Task<bool> Login(User user, string pass);
    }

}

