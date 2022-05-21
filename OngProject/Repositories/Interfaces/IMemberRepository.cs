using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        void Delete(Member entity);
    }

}

