using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Seeder;
using OngProject.DataAccess.Seeder.Activities;
using OngProject.DataAccess.Seeder.News;
using OngProject.DataAccess.Seeder.Organizations;
using OngProject.DataAccess.Seeder.Roles;
using OngProject.DataAccess.Seeder.Testimonials;
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
             modelBuilder.Entity<News>()
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

            // Implement seed data
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new TestimonialConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
        }

        public DbSet<Organization> Organizations { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<News> News { set; get; }
        public DbSet<Member> Members {set;get;}
        public DbSet<User> Users { set; get;}
        public DbSet<Role> Roles { set; get; }
        public DbSet<Activity> Activities { set; get; }
        public DbSet<Testimonial> Testimonials { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Comment> Comments { set; get; }
    }
}