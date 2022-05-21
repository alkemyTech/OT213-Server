using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private OngProjectDbContext _ongProjectDbContext;
        
        public GenericRepository(OngProjectDbContext ongProjectDbContext)
        {
            this._ongProjectDbContext = ongProjectDbContext;
        }
        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if(entity == null)
                throw new Exception("The entity is null");

            _ongProjectDbContext.Set<TEntity>();//.Remove(entity);
            await _ongProjectDbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _ongProjectDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _ongProjectDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _ongProjectDbContext.Set<TEntity>().AddAsync(entity);
            await _ongProjectDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _ongProjectDbContext.Set<TEntity>().Update(entity);
            await _ongProjectDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
