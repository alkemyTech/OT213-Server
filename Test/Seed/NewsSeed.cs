using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class NewsSeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new News
                {
                    Id = 1,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 1
                });

            context.Add(
                new News
                {
                    Id = 2,
                    Name = "No news",
                    Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                    Content = "No news, good news",
                    CategoryId = 2
                });
        }
    }
}
