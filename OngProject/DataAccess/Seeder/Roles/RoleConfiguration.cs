using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(n => n.Description)
                    .HasMaxLength(254);

            builder.HasData(
                new Entities.Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Administrator"
                },
                new Entities.Role
                {
                    Id = 2,
                    Name = "Owner",
                    Description = "Owner"
                }
            );
        }
    }
}

