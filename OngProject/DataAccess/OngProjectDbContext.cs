using Microsoft.EntityFrameworkCore;
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

            // Property Configurations 
            modelBuilder.Entity<Member>()
                        .Property(c => c.Id)
                        .IsRequired();   

            modelBuilder.Entity<Member>()
                        .Property(c => c.Name)
                        .HasMaxLength(50)
                        .IsRequired();
            
            modelBuilder.Entity<Member>()
                        .Property(c => c.ImageUrl)
                        .IsRequired();

            modelBuilder.Entity<Member>()
                        .Property(c => c.Description)
                        .HasMaxLength(50);
        }

        public DbSet<Organization> Organizations { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<New> News { set; get; }
        public DbSet<Member> Members {set;get;}
        public DbSet<User> Users { set; get;}
        public DbSet<Role> Roles { set; get; }
        public DbSet<Activities> Activities { set; get; }
        public DbSet<Testimonial> Testimonials { set; get; }
    }
}


