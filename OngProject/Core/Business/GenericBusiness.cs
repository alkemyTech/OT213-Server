using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class GenericBusiness<TEntity> : IGenericBusiness<TEntity> where TEntity : class
    {
        private IGenericRepository<TEntity> _genericRepository;
        private IUnitOfWork _uow;

        public GenericBusiness(IGenericRepository<TEntity> genericRepository, IUnitOfWork uow)
        {
            this._genericRepository = genericRepository;
            this._uow = uow;
        }

        // Hard Delete
        public async Task Delete(int id)
        {
            await _genericRepository.Delete(id);
            await _uow.SaveAsync();
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
            var insert = await _genericRepository.Insert(entity);
            await _uow.SaveAsync();
            return insert;
        }

        public async Task<bool> SoftDelete(TEntity entity, int? id)
        {
            var softDelete = await _genericRepository.SoftDelete(entity, id);
            await _uow.SaveAsync();
            return softDelete;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var update = await _genericRepository.Update(entity);
            await _uow.SaveAsync();
            return update;
        }
    }

}
