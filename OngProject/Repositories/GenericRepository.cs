using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

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

            if(entity == null)
                throw new Exception("The entity is null");

            context.Set<TEntity>().Remove(entity);
        }

        IEnumerable<TEntity> IGenericRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;

            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTime.Now;
            context.Set<TEntity>().Update(entity);
            return entity;
        }

        // Soft Delete
        public async Task<bool> SoftDelete(TEntity entity, int? id)
        {
            try
            {
                /*
                    the "?" in int? set that int can be nulleable
                    ! (null-forgiving) operator to confirm that "id" isn't null here
                    If "value" isn't null return "isDeleted" as true.
                */
                var value = await GetById(id!.Value); 
                if(value == null)
                    throw new Exception("The entity is null"); 

                var Ent = entity.IsDeleted = true; 
                return entity.IsDeleted = true;                    
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
