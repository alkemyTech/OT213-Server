using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Registrar(User user, string pass);
        Task<bool> ExistsUser(string email);
        Task<bool> Login(User user, string pass);
    }
}

