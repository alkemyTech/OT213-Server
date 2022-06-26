using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OngProject.DataAccess.Seeder.Organizations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Entities.Organization>
    {
        public void Configure(EntityTypeBuilder<Entities.Organization> builder)
        {
            // Property Configurations 
            builder.ToTable("Organizations");
            builder.Property(n => n.Id)
                    .IsRequired();

            builder.Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.HasData(
                new Entities.Organization
                {
                    Id = 1,
                    Name = "Somos Más",
                    Image = "Somos Más",
                    Address = "Somos Más",
                    Phone = 1160112988,
                    Email = "somosfundacionmas@gmail.com",
                    Welcome = "Somos Más",
                    AboutUs = "Somos Más",
                    FacebookUrl = "Somos_Más",
                    InstagramUrl = "Somos_Más",
                    LinkedInUrl = "Somos Más"
                }
            );
        }
    }
}

