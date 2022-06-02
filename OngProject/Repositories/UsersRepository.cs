using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using OngProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OngProject.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(OngProjectDbContext context) : base(context)
        {
        }
        public async Task<bool> SoftDelete(Users entity, int? id)
        {
            try
            {
                var value = await GetById(id!.Value);
                if (value == null)
                    throw new Exception("The entity is null");

                return entity.softDelete = true;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<Users> UpdateUsersAsync(Users entity) { }
        public Task<Users> GetUsersByIdAsync(int id) { }
        public Task<Users> InsertUsersAsync(Users entity) { }
        public IEnumerable<Users> FindUsersAsync(Expression<Func<Users, bool>> predicate) { }
    }
}
