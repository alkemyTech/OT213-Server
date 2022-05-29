using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness : IGenericBusiness<News>
    {
        Task<bool> SoftDelete(News entity, int? id);

        Task<News> UpdateNewsAsync(News entity);
        Task<News> GetNewsByIdAsync(int id);
        Task<News> InsertNewsAsync(News entity);
        IEnumerable<News> FindNewsAsync(Expression<Func<News, bool>> predicate);
    }
}
