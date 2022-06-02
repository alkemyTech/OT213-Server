using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using OngProject.DataAccess;
using System.Linq.Expressions;

namespace OngProject.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(OngProjectDbContext context) : base(context)
        {
        }
        public async Task<bool> SoftDelete(News entity, int? id)
        {
            try
            {
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
        public Task<News> UpdateNewsAsync(News entity) { }
        public Task<News> GetNewsByIdAsync(int id) { }
        public Task<News> InsertNewsAsync(News entity) { }
        public IEnumerable<News> FindNewsAsync(Expression<Func<News, bool>> predicate) { }
    }
}
