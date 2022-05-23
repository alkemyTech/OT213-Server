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

        // Soft Delete
        public async Task<bool> SoftDelete(Member entity, int? id)
        {
            try
            {
                /*
                    int? set that int can be nulleable
                    ! (null-forgiving) operator to confirm that "id" isn't null here
                    If "value" isn't null return the "SoftDelete" as true.
                */

                //var Ent = (IMemberRepository)genericContext.Set<Member>().Find(id!.Value);
                var value = await GetById(id!.Value); 
                if(value == null)
                    throw new Exception("The entity is null");

                return entity.SoftDelete = true;                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Soft Delete method
        public async Task SoftDelete2(Member entity, int? id)
        {
            try
            {
                // ! (null-forgiving) operator to confirm that "id" isn't null here
                var value = await GetById(id!.Value); 

                if(id == null)
                    throw new Exception("The entity is null");                

                /* 
                    Get the name of the attribute from the entity
                    Get the value of the attribute and then ask if hasn't a value, set a new value.
                */
                var property = entity.GetType().GetProperty("SoftDelete");
                var propertyValue = (bool?)property?.GetValue(entity);
                if (!propertyValue.HasValue)
                {
                    property?.SetValue(entity, true);
                }                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        // // Soft Delete method
        // void IMemberRepository.SoftDelete(Member entity, int? id)
        // {
        //     try
        //     {
        //         var value = GetById(id!.Value);

        //         if(id == null){
        //             throw new Exception("The entity is null");
        //         }

        //         var property = entity.GetType().GetProperty("SoftDelete");
        //         var propertyValue = (bool?)property?.GetValue(entity);
        //         if (!propertyValue.HasValue)
        //         {
        //             property?.SetValue(entity, true);
        //         }
                
        //     }
        //     catch (System.Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }

       
    }

}

