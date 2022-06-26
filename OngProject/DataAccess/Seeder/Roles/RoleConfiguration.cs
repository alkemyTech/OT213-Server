using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;


namespace OngProject.DataAccess.Seeder.Roles
{
    public class RoleConfiguration : IEntityTypeConfiguration<Entities.Role>
    {
        public void Configure(EntityTypeBuilder<Entities.Role> builder)
        {
            // Property Configurations 
            builder.ToTable("Roles");
            builder.Property(n => n.Id)
                    .IsRequired();

            // Populate the table Roles
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

