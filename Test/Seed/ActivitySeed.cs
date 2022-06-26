using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class ActivitySeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new Activity
                {
                    Id = 1,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/c0b7ce93-2d00-11e6-bce7-6ff134176666.jpg",
                    Content = "Attending sporting events"
                });

            context.Add(
                new Activity
                {
                    Id = 2,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg",
                    Content = "Puppetry"
                });

            context.Add(
                new Activity
                {
                    Id = 3,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/3470a674-6a05-11ea-b459-9d2edb98bc96.jpg",
                    Content = "Going to the park"
                });

            context.Add(
                new Activity
                {
                    Id = 4,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/340eb0ee-6a05-11ea-b459-9d2edb98bc96.jpg",
                    Content = "Going to concerts"
                });

            context.Add(
                new Activity
                {
                    Id = 5,
                    Name = "(Social Activities)",
                    Image = "https://d3hjzzsa8cr26l.cloudfront.net/cfe95939-2d0d-11e6-a4bd-71dbf5f2854a.jpg",
                    Content = "Volunteering"
                });
        }
    }
}
