using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class MemberSeed
    {
        public static void Seed(OngProjectDbContext context)
        {
            context.Add(
                new Member
                {
                    Id = 1,
                    Name = "Dave Mustaine",
                    FacebookUrl = "https://www.facebook.com/daveMust22",
                    InstagramUrl = "https://www.instagram.com/daveMust22",
                    LinkedInUrl = "https://www.linkedin.com/in/dave-mustaine",
                    ImageUrl = "https://gcdn.emol.cl/rock/files/2019/09/megadeth-dave-mustaine.jpg",
                    Description = "Miembro activo de la organización"
                });
            context.Add(
                new Member
                {
                    Id = 2,
                    Name = "John Petrucci",
                    FacebookUrl = "https://www.facebook.com/johnPetru1",
                    InstagramUrl = "https://www.instagram.com/johnPetru1",
                    LinkedInUrl = "https://www.linkedin.com/in/john-petrucci",
                    ImageUrl = "https://magazyngitarzysta.pl/i/images/9/7/6/dz0yNTE4Jmg9MzAwMA==_src_140976-GettyImages-911852516.jpg",
                    Description = "Miembro activo de la organización"
                });
        }
    }
}
