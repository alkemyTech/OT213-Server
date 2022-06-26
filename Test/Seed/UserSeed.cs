using OngProject.DataAccess;
using OngProject.Entities;
using System.Text;

namespace Test.Seed
{
    public static class UserSeed
    {
        public static void Seed(OngProjectDbContext context) {
            Encoding e = Encoding.Unicode;

            context.Add(
                new User
                {
                    Id = 1,
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    Email = "Email1@email.com",
                    Password = "Password1",
                    Photo = "Photo1",
                    PasswordHash = e.GetBytes("PasswordHash1"),
                    PasswordSalt = e.GetBytes("PasswordSalt1"),
                    RoleId = 2
                });

            context.Add(
                new User
                {
                    Id = 2,
                    FirstName = "FirstName 2 ",
                    LastName = "LastName 2",
                    Email = "Email2@email.com",
                    Password = "Password2",
                    Photo = "Photo2",
                    PasswordHash = e.GetBytes("PasswordHash2"),
                    PasswordSalt = e.GetBytes("PasswordSalt2"),
                    RoleId = 2
                });

        }
    }
}