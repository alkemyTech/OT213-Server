using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : GenericBusiness<Categories>, ICategoriesBusiness
    {
        private IUnitOfWork _uow;
        private ICategoriesRepository _categoriesRepository;
        public CategoriesBusiness(IUnitOfWork uow, ICategoriesRepository categoriesRepository) : base(categoriesRepository)
        {
            this._uow = uow;
            this._categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Categories> FindCategoryAsync(Expression<Func<Categories, bool>> predicate)
        {
            return _uow.Categories.FindCategoryAsync(predicate);
        }

        public Task<Categories> GetCategoryByIdAsync(int id)
        {
            return _uow.Categories.GetCategoryByIdAsync(id);
        }

        public Task<Categories> InsertCategoryAsync(Categories entity)
        {
            return _uow.Categories.InsertCategoryAsync(entity);
        }

        // Soft Delete
        public async Task<bool> SoftDelete(Categories entity, int? id)
        {
            return await _uow.Categories.SoftDelete(entity, id);
        }

        public async Task<Categories> UpdateCategoryAsync(Categories entity)
        {
            return await _uow.Categories.UpdateCategoryAsync(entity);
        }
    }
}