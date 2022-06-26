using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using Test.Seed;

namespace Test.Helper
{
    public class OngProjectDbContextMemory
    {
        private static OngProjectDbContext _context;
        public static OngProjectDbContext MakeDbContext()
        {
            var options = new DbContextOptionsBuilder<OngProjectDbContext>().UseInMemoryDatabase(databaseName: "OngProject").Options;
            _context = new OngProjectDbContext(options);
            _context.Database.EnsureDeleted();


            ActivitySeed.Seed(_context);
            CategorySeed.Seed(_context);
            CommentSeed.Seed(_context);
            MemberSeed.Seed(_context);
            NewsSeed.Seed(_context);
            OrganizationSeed.Seed(_context);
            RoleSeed.Seed(_context);
            SlideSeed.Seed(_context);
            TestimonialSeed.Seed(_context);
            UserSeed.Seed(_context);

            _context.SaveChanges();

            return _context;
        }
    }
}
