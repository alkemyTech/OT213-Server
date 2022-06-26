using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class SlideSeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new Slide
                {
                    Id = 1,
                    Name = "Name",
                    Text = "Text",
                    ImageUrl = "ImageUrl",
                    Order = 1,
                    OrganizationId = 1
                });

            context.Add(
                new Slide
                {
                    Id = 2,
                    Name = "Name",
                    Text = "Text",
                    ImageUrl = "ImageUrl",
                    Order = 1,
                    OrganizationId = 2
                });
        }
    }
}
