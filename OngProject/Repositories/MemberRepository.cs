using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        /*
            Protected DbContext mapping in GenericRepository.cs as OngProjectDbContext.
        */ 
        public OngProjectDbContext OngProjectDbContext
        {
            get{return genericContext as OngProjectDbContext;}
        }

        public IEnumerable<Member> FindMemberAsync(Expression<Func<Member, bool>> predicate)
        {
            return genericContext.Set<Member>().Where(predicate);
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await genericContext.Set<Member>().FindAsync(id);
        }

        public async Task<Member> InsertMemberAsync(Member entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await genericContext.Set<Member>().AddAsync(entity);
            return entity;
        }

        // Soft Delete
        public async Task<bool> SoftDelete(Member entity, int? id)
        {
            try
            {
                /*
                    the "?" in int? set that int can be nulleable
                    ! (null-forgiving) operator to confirm that "id" isn't null here
                    If "value" isn't null return "isDeleted" as true.
                */
                var value = await GetById(id!.Value); 
                if(value == null)
                    throw new Exception("The entity is null");              

                return entity.isDeleted = true;                 
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Member> UpdateMemberAsync(Member entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTime.Now;
            genericContext.Set<Member>().Update(entity);
            return entity;
        }
    }

}

