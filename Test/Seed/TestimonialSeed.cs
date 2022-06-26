using OngProject.DataAccess;
using OngProject.Entities;

namespace Test.Seed
{
    public static class TestimonialSeed
    {
        public static void Seed(OngProjectDbContext context) {
            context.Add(
                new Testimonial
                {
                    Id = 1,
                    Name = "Florence F Brooks",
                    Image = "https://www.fakepersongenerator.com/Face/female/female20111023425786712.jpg",
                    Description = "Just amazing. Thank You! The very best. Ong Project Alkemy is the real deal!"
                });

            context.Add(
                new Testimonial
                {
                    Id = 2,
                    Name = "Kathie D Green",
                    Image = "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg",
                    Description = "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company."
                });

        }
    }
}
