using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OngProject.DataAccess.Seeder.News
{
    public class NewsConfiguration : IEntityTypeConfiguration<Entities.News>
    {
        public void Configure(EntityTypeBuilder<Entities.News> builder)
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

            builder.HasData(
                new Entities.News
                {
                    Id = 1,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 1
                },
                new Entities.News
                {
                    Id = 2,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 2

                },
                new Entities.News
                {
                    Id = 3,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 3
                },
                new Entities.News
                {
                    Id = 4,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 4
                },
                new Entities.News
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

