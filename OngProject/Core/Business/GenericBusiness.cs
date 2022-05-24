using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class GenericBusiness<TEntity> : IGenericBusiness<TEntity> where TEntity : class
    {

        private IGenericRepository<TEntity> _genericRepository;

        public GenericBusiness(IGenericRepository<TEntity> genericRepository)
        {
            this._genericRepository = genericRepository;
        }

        // Hard Delete
        public async Task Delete(int id)
        {
            await _genericRepository.Delete(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _genericRepository.Find(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _genericRepository.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _genericRepository.GetById(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            return await _genericRepository.Insert(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return await _genericRepository.Update(entity);
        }
    }

}
