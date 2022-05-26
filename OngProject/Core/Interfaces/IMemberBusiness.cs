using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness : IGenericBusiness<Member>
    {        
        // Soft Delete
        Task<bool> SoftDelete(Member entity, int? id);

        Task<Member> UpdateMemberAsync(Member entity);  
        Task<Member> GetMemberByIdAsync(int id);  
        Task<Member> InsertMemberAsync(Member entity);
        IEnumerable<Member> FindMemberAsync(Expression<Func<Member, bool>> predicate);

    }
}
