using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUsersBusiness : IGenericBusiness<Users>
    {
        Task<bool> SoftDelete(Users entity, int? id);

        Task<Users> UpdateUsersAsync(Users entity);
        Task<Users> GetUsersByIdAsync(int id);
        Task<Users> InsertUsersAsync(Users entity);
        IEnumerable<Users> FindUsersAsync(Expression<Func<Users, bool>> predicate);
    }
}
