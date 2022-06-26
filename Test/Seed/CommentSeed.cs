using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class CommentSeed
    {
        public static void Seed(OngProjectDbContext context)
        {
            context.Add(
                new Comment
                {
                    Id = 1,
                    NewId = 1,
                    Body = "Test",
                    UserId = 1
                });
            context.Add(
                new Comment
                {
                    Id = 2,
                    NewId = 1,
                    Body = "Test 2",
                    UserId = 2
                });
        }
    }
}
