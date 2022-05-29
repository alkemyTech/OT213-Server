using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : GenericBusiness<News>, INewsBusiness
    {
        private IUnitOfWork _uow;
        private INewsRepository _newsRepository;
        public NewsBusiness(IUnitOfWork uow, INewsRepository newsRepository) : base(newsRepository)
        {
            this._uow = uow;
            this._newsRepository = newsRepository;
        }

        public IEnumerable<News> FindNewsAsync(Expression<Func<News, bool>> predicate)
        {
            return _uow.News.FindNewsAsync(predicate);
        }

        public Task<News> GetNewsByIdAsync(int id)
        {
            return _uow.News.GetNewsByIdAsync(id);
        }

        public Task<News> InsertNewsAsync(News entity)
        {
            return _uow.News.InsertNewsAsync(entity);
        }

        public async Task<bool> SoftDelete(News entity, int? id)
        {
            return await _uow.News.SoftDelete(entity, id);
        }

        public async Task<News> UpdateNewsAsync(News entity)
        {
            return await _uow.News.UpdateNewsAsync(entity);
        }
    }
}
