using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class RoleSeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                });

            context.Add(
                new Role
                {
                    Id = 2,
                    Name = "Owner"
                });
        }
    }
}
