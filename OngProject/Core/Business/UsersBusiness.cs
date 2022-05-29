using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UsersBusiness : GenericBusiness<Users>, IUsersBusiness
    {
        private IUnitOfWork _uow;
        private IUsersRepository _usersRepository;
        public UsersBusiness(IUnitOfWork uow, IUsersRepository usersRepository) : base(usersRepository)
        {
            this._uow = uow;
            this._usersRepository = usersRepository;
        }

        public IEnumerable<Users> FindUsersAsync(Expression<Func<Users, bool>> predicate)
        {
            return _uow.Users.FindUsersAsync(predicate);
        }

        public Task<Users> GetUsersByIdAsync(int id)
        {
            return _uow.Users.GetUsersByIdAsync(id);
        }

        public Task<Users> InsertUsersAsync(Users entity)
        {
            return _uow.Users.InsertUsersAsync(entity);
        }

        public async Task<bool> SoftDelete(Users entity, int? id)
        {
            return await _uow.Users.SoftDelete(entity, id);
        }

        public async Task<Users> UpdateUsersAsync(Users entity)
        {
            return await _uow.Users.UpdateUsersAsync(entity);
        }
    }
}
