using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface ICategoriesRepository : IGenericRepository<Categories>
    {
        // Soft Delete
        Task<bool> SoftDelete(Categories entity, int? id);

        Task<Categories> UpdateCategoryAsync(Categories entity);  
        Task<Categories> GetCategoryByIdAsync(int id);   
        Task<Categories> InsertCategoryAsync(Categories entity);
        IEnumerable<Categories> FindCategoryAsync(Expression<Func<Categories, bool>> predicate);



    }

}

