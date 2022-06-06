using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeder.Testimonials
{
    public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
    {
        public void Configure(EntityTypeBuilder<Testimonial> builder)
        {
            builder.ToTable("Testimonials");
            builder.Property(testimonial => testimonial.Id)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Image)
                    .IsRequired();

            builder.Property(testimonial => testimonial.Description)
                    .HasMaxLength(254);

              builder.HasData(
                new Testimonial
                {
                    Id = 1,
                    Name = "Florence F Brooks",
                    Image = "https://www.fakepersongenerator.com/Face/female/female20111023425786712.jpg",
                    Description = "Just amazing. Thank You! The very best. Ong Project Alkemy is the real deal!"
                },
                new Testimonial
                {
                    Id = 2,
                    Name = "Kathie D Green",
                    Image = "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg",
                    Description = "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company."
                },
                new Testimonial
                {
                    Id = 3,
                    Name = "Claude V Patterson",
                    Image = "https://www.fakepersongenerator.com/Face/male/male1084237525421.jpg",
                    Description = "Ong Project Alkemy impressed me on multiple levels. Ong Project Alkemy is the real deal!"
                },
                new Testimonial
                {
                    Id = 4,
                    Name = "David R Andrews",
                    Image = "https://www.fakepersongenerator.com/Face/male/male20151086250510345.jpg",
                    Description = "Ong Project Alkemy is worth much more than I paid."
                },
                new Testimonial
                {
                    Id = 5,
                    Name = "Frederick I Giroux",
                    Image = "https://www.fakepersongenerator.com/Face/male/male1085177699859.jpg",
                    Description = "We were treated like royalty."
                }
            );
        }
    }


}

