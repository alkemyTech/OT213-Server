using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness : IGenericBusiness<Member>
    {        
        //void SoftDelete(Member entity);

        // Soft Delete
        Task<bool> SoftDelete(Member entity, int? id);

    }
}
