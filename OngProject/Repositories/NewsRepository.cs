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
    public class NewsRepository : GenericRepository<New>, INewsRepository
    {
        public NewsRepository(OngProjectDbContext context) : base(context)
        {
        }       
    }
}
