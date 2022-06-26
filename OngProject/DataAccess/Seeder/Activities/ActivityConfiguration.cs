using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OngProject.DataAccess.Seeder.Activities
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Entities.Activity>
    {
        public void Configure(EntityTypeBuilder<Entities.Activity> builder)
        {
            builder.ToTable("Activities");
            builder.Property(testimonial => testimonial.Id)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Image)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Content)
                    .HasMaxLength(254);

              builder.HasData(
                new Entities.Activity
                {
                    Id = 1,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/c0b7ce93-2d00-11e6-bce7-6ff134176666.jpg",
                    Content = "Attending sporting events"
                },
                new Entities.Activity
                {
                    Id = 2,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg",
                    Content = "Puppetry"
                },
                new Entities.Activity
                {
                    Id = 3,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/3470a674-6a05-11ea-b459-9d2edb98bc96.jpg",
                    Content = "Going to the park"
                },
                new Entities.Activity
                {
                    Id = 4,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/340eb0ee-6a05-11ea-b459-9d2edb98bc96.jpg",
                    Content = "Going to concerts"
                },
                new Entities.Activity
                {
                    Id = 5,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/cfe95939-2d0d-11e6-a4bd-71dbf5f2854a.jpg",
                    Content = "Volunteering"
                }
            );
        }
    }


}

