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
    public class UsersBusiness : GenericBusiness<User>, IUsersBusiness
    {
        private IUnitOfWork _uow;
        private IUsersRepository _usersRepository;
        public UsersBusiness(IUnitOfWork uow, IUsersRepository usersRepository) : base(usersRepository, uow)
        {
            this._uow = uow;
            this._usersRepository = usersRepository;
        }

        public IEnumerable<User> FindUsersAsync(Expression<Func<User, bool>> predicate)
        {
            return _uow.Users.Find(predicate);
        }

        public Task<User> GetUsersByIdAsync(int id)
        {
            return _uow.Users.GetById(id);
        }

        public Task<User> InsertUsersAsync(User entity)
        {
            return _uow.Users.Insert(entity);
        }

        public async Task<bool> SoftDelete(User entity, int? id)
        {
            return await _uow.Users.SoftDelete(entity, id);
        }

        public async Task<User> UpdateUsersAsync(User entity)
        {
            return await _uow.Users.Update(entity);
        }
    }
}
