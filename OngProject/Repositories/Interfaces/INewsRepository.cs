using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        Task<bool> SoftDelete(News entity, int? id);
    }
}
