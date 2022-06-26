using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class OrganizationSeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new Organization
                {
                    Id = 1,
                    Name = "Ong Somos Más",
                    Image = "Image",
                    Email = "Email",
                    Welcome = "Welcome",
                    Address = "Address",
                    Phone = 123,
                    AboutUs = "AboutUs",
                    FacebookUrl = "FacebookUrl",
                    InstagramUrl = "InstagramUrl",
                    LinkedInUrl = "LinkedInUrl"
                });
        }
    }
}
