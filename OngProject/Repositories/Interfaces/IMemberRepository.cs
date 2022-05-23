using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        //void SoftDelete(Member entity, int? id);
        Task SoftDelete2(Member entity, int? id);

        // Soft Delete
        Task<bool> SoftDelete(Member entity, int? id);
    }

}

