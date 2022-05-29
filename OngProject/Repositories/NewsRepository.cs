using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using OngProject.DataAccess;

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
                /*
                    the "?" in int? set that int can be nulleable
                    ! (null-forgiving) operator to confirm that "id" isn't null here
                    If "value" isn't null return "isDeleted" as true.
                */
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
    }
}
