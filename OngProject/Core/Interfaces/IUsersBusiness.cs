using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUsersBusiness : IGenericBusiness<User>
    {
        Task<bool> SoftDelete(User entity, int? id);

        Task<User> UpdateUsersAsync(User entity);
        Task<User> GetUsersByIdAsync(int id);
        Task<User> InsertUsersAsync(User entity);
        IEnumerable<User> FindUsersAsync(Expression<Func<User, bool>> predicate);
    }
}
