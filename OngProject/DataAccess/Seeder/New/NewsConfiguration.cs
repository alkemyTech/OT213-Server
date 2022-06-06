using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OngProject.DataAccess.Seeder.New
{
    public class NewsConfiguration : IEntityTypeConfiguration<Entities.New>
    {
        public void Configure(EntityTypeBuilder<Entities.New> builder)
        {
            // Property Configurations 
            builder.ToTable("News");
            builder.Property(n => n.Id)
                    .IsRequired();

            builder.Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(n => n.Image)
                    .IsRequired();

            builder.Property(n => n.Content)
                    .HasMaxLength(254);

            // Populate the table Members
            builder.HasData(
                new Entities.New
                {
                    Id = 1,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",

                    Content = "No news, good news",
                    CategoryId = 1
                },
                new Entities.New
                {
                    Id = 2,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 2

                },
                new Entities.New
                {
                    Id = 3,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 3
                },
                new Entities.New
                {
                    Id = 4,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 4
                },
                new Entities.New
                {
                    Id = 5,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 4
                }
            );
        }
    }


}

