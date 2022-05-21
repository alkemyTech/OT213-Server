using System.Data.Common;
using System;
using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(OngProjectDbContext context) : base(context)
        {            
        }

        // Protected DbContext mapping in GenericRepository.cs as OngProjectDbContext.
        public OngProjectDbContext OngProjectDbContext
        {
            get{return genericContext as OngProjectDbContext;}
        }

        void IMemberRepository.Delete(Member entity)
        {
            var property = entity.GetType().GetProperty("SoftDelete");
            var propertyValue = (bool?)property?.GetValue(entity);
            if (!propertyValue.HasValue)
            {
                property?.SetValue(entity, true);
            }
        }

       
    }

}

