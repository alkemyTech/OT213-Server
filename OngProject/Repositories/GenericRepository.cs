using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /* 
            OngProjectDbContext is implemented in UnitOfWork class
            A generic DbContext implemented here. 
            Is protetected to be implemented in each entity repository. (e.g. MemberReposirory.cs)
        */
        protected DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        // Hard Delete
        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            context.Set<TEntity>().Remove(entity);
        }

        IEnumerable<TEntity> IGenericRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).OrderBy(x => x.CreatedAt);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;

            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            context.Set<TEntity>().Update(entity);
            return entity;
        }

        // Soft Delete
        public async Task<bool> SoftDelete(TEntity entity)
        {
            var value = await GetById(entity.Id);
            return entity.IsDeleted = true;
        }
    }
}
