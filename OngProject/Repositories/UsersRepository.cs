using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using OngProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OngProject.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(OngProjectDbContext context) : base(context)
        {
        }
        
        
    }
}
