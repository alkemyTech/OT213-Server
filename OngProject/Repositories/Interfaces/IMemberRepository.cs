using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        // Soft Delete
        Task<bool> SoftDelete(Member entity, int? id);
    }

}

