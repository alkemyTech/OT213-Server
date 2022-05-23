using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /* 
            OngProjectDbContext is implemented in UnitOfWork class
            A generic DbContext implemented here. 
            Is protetected to be implemented in each entity repository. (e.g. MemberReposirory.cs)
        */
        protected DbContext genericContext;
        
        public GenericRepository(DbContext context)
        {
            this.genericContext = context;
        }

        // Hard Delete
        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if(entity == null)
                throw new Exception("The entity is null");

            genericContext.Set<TEntity>().Remove(entity);
            await genericContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await genericContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await genericContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await genericContext.Set<TEntity>().AddAsync(entity);
            //await genericContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            genericContext.Set<TEntity>().Update(entity);
            //await genericContext.SaveChangesAsync();
            return entity;
        }
    }
}
