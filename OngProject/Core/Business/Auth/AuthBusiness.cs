using System.Threading.Tasks;
using OngProject.Core.Auth.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Auth.Interfaces;

namespace OngProject.Core.Business.Auth
{
    public class AuthBusiness : IAuthBusiness
    {
        private IAuthRepository _authRepo;
        private IUnitOfWork _uow;
        public AuthBusiness(IUnitOfWork uow, IAuthRepository authRepo)
        {
            this._authRepo = authRepo;
            this._uow = uow;
        }
        public async Task<bool> ExistsUser(string email)
        {
            var exists = await _authRepo.ExistsUser(email);
            await _uow.SaveAsync();
            return exists; 
        }
        public async Task<User> Registrar(User user, string pass)
        {
            var register = await _authRepo.Registrar(user, pass);
            await _uow.SaveAsync();
            return register;
        }
    }

}

