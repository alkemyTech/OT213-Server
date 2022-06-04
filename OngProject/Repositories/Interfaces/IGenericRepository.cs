using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OngProject.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);               
        
        // Hard Delete
        Task Delete(int id);

        // Soft Delete
        Task<bool> SoftDelete(TEntity entity, int? id);
    }
}
