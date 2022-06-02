using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeder
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Property Configurations 
            builder.ToTable("Members");
            builder.Property(c => c.Id)
                    .IsRequired();   

            builder.Property(c => c.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            
            builder.Property(c => c.ImageUrl)
                    .IsRequired();

            builder.Property(c => c.Description)
                    .HasMaxLength(50);

            // Populate the table Members
            builder.HasData(
                new Member
                {
                    Id = 1,
                    Name = "Dave Mustaine",
                    FacebookUrl = "https://www.facebook.com/daveMust22",
                    InstagramUrl = "https://www.instagram.com/daveMust22",
                    LinkedInUrl = "https://www.linkedin.com/in/dave-mustaine",
                    ImageUrl = "https://gcdn.emol.cl/rock/files/2019/09/megadeth-dave-mustaine.jpg",
                    Description = "Miembro activo de la organización"
                },
                new Member
                {
                    Id = 2,
                    Name = "John Petrucci",
                    FacebookUrl = "https://www.facebook.com/johnPetru1",
                    InstagramUrl = "https://www.instagram.com/johnPetru1",
                    LinkedInUrl = "https://www.linkedin.com/in/john-petrucci",
                    ImageUrl = "https://magazyngitarzysta.pl/i/images/9/7/6/dz0yNTE4Jmg9MzAwMA==_src_140976-GettyImages-911852516.jpg",
                    Description = "Miembro activo de la organización"
                },
                new Member
                {
                    Id = 3,
                    Name = "Steve Vai",
                    FacebookUrl = "https://www.facebook.com/steveVai66",
                    InstagramUrl = "https://www.instagram.com/steveVai66",
                    LinkedInUrl = "https://www.linkedin.com/in/steve-vai",
                    ImageUrl = "https://273710-849646-raikfcquaxqncofqfm.stackpathdns.com/wp-content/uploads/2012/07/Steve-Vai-pic.jpg",
                    Description = "Miembro activo de la organización"
                },
                new Member
                {
                    Id = 4,
                    Name = "Joe Satriani",
                    FacebookUrl = "https://www.facebook.com/joeStriani1",
                    InstagramUrl = "https://www.instagram.com/joeStriani1",
                    LinkedInUrl = "https://www.linkedin.com/in/joe-satriani",
                    ImageUrl = "https://th.bing.com/th/id/OIP.3X4EMm2OGqVqR77JQvJzagAAAA?pid=ImgDet&rs=1",
                    Description = "Miembro activo de la organización"
                }
            );
        }
    }


}

