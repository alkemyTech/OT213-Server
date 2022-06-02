using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class CategoriesRepository : GenericRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(OngProjectDbContext context) : base(context)
        {
        }

        /*
            Protected DbContext mapping in GenericRepository.cs as OngProjectDbContext.
        */
        public OngProjectDbContext OngProjectDbContext
        {
            get { return genericContext as OngProjectDbContext; }
        }

        public IEnumerable<Categories> FindCategoryAsync(Expression<Func<Categories, bool>> predicate)
        {
            return genericContext.Set<Categories>().Where(predicate);
        }

        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
            return await genericContext.Set<Categories>().FindAsync(id);
        }

        public async Task<Categories> InsertCategoryAsync(Categories entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await genericContext.Set<Categories>().AddAsync(entity);
            return entity;
        }

        // Soft Delete
        public async Task<bool> SoftDelete(Categories entity, int? id)
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
        public async Task<Categories> UpdateCategoryAsync(Categories entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTime.Now;
            genericContext.Set<Categories>().Update(entity);
            return entity;
        }

    }

}