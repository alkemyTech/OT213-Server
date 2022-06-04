using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Seeder;
using OngProject.Entities;

namespace OngProject.DataAccess
{
    public class OngProjectDbContext : DbContext
    {
        public OngProjectDbContext(DbContextOptions<OngProjectDbContext> options) : base(options)
        {  

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            // Fluent Api                
            // configure one-to-many relationship
             modelBuilder.Entity<New>()
                .HasOne<Category>(u => u.Category)
                .WithMany(c => c.News)
                .HasForeignKey(u => u.CategoryId);

            modelBuilder.Entity<User>()
                .HasOne<Role>(u => u.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Slide>()
                .HasOne<Organization>(u => u.Organization)
                .WithMany(c => c.Slides)
                .HasForeignKey(u => u.OrganizationId);

            // Implement seed data from members
            modelBuilder.ApplyConfiguration(new MemberConfiguration());

        }

        public DbSet<Organization> Organizations { set; get; }
<<<<<<< HEAD
        public DbSet<Roles> Roles { set; get;}
        public DbSet<News> News { set; get;}
        public DbSet<Users> Users { set; get;}
=======
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<New> News { set; get; }
        public DbSet<Member> Members {set;get;}
        public DbSet<User> Users { set; get;}
        public DbSet<Role> Roles { set; get; }
        public DbSet<Activities> Activities { set; get; }
        public DbSet<Testimonial> Testimonials { set; get; }
>>>>>>> e38780518ca654d41d3d3b361602e9fd3f15b52b
    }
}


