using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using OngProject.DataAccess;
using System;

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
                /*
                    the "?" in int? set that int can be nulleable
                    ! (null-forgiving) operator to confirm that "id" isn't null here
                    If "value" isn't null return "isDeleted" as true.
                */
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
    }
}
