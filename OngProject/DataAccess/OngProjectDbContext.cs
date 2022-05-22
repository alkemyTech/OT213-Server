using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("OngProjectConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            // Fluent Api
            // Property Configurations    
            modelBuilder.Entity<Member>()
                        .Property(c => c.MembersID)
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

        public DbSet<Member> Members {set;get;}
    }
}


