using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);               
        
        // Hard Delete
        Task Delete(int id);
    }
}
