using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeder
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Property Configurations 
            builder.ToTable("Categories");
            builder.Property(c => c.Id)
                    .IsRequired();

            builder.Property(c => c.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(c => c.Image)
                    .IsRequired();

            builder.Property(c => c.Description)
                    .HasMaxLength(50);

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Animals and pets",
                    Image = "https://www.jg-cdn.com/assets/jg-homepage/339d41967797c7d4f41a0addcc659196.svg",
                    Description = "Animals and pets"
                },
                new Category
                {
                    Id = 2,
                    Name = "Art and culture",
                    Image = "https://www.jg-cdn.com/assets/jg-homepage/187aa4c1ceded5ddeab457ba26c1eb65.svg",
                    Description = "Art and culture"
                },
                new Category
                {
                    Id = 3,
                    Name = "Education",
                    Image = "https://www.jg-cdn.com/assets/jg-homepage/7d2169169f7d1316cec9b5733dd59718.svg",
                    Description = "Education"
                },
                new Category
                {
                    Id = 4,
                    Name = "International aid",
                    Image = "https://www.jg-cdn.com/assets/jg-homepage/675f647d01d3751a0113d305ce2baf8e.svg",
                    Description = "International aid"
                },
                new Category
                {
                    Id = 5,
                    Name = "Disability",
                    Image = "https://www.jg-cdn.com/assets/jg-homepage/cec5dbbde623a23c0e7239e969a366d1.svg",
                    Description = "Disability"
                }
            );
        }
    }


}

