using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeder.Roles
{
    public class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Property Configurations 
            builder.ToTable("Roles");
           
            // Populate the table Users
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "User with access to all resources."            
                },
                new Role
                {
                    Id = 2,
                    Name = "Owner",
                    Description = "User with limited access."            
                }
            );
        }
    }

}

