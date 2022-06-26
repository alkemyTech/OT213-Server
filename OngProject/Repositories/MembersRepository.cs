using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class MembersRepository : GenericRepository<Member>, IMembersRepository
    {
        public MembersRepository(OngProjectDbContext context) : base(context) {}
    }

}

