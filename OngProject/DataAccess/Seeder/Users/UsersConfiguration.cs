using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeder.Users
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        //private PasswordHashingService _passHash;
        // public UsersConfiguration(PasswordHashingService passHash)
        // {
        //     //this._passHash = passHash;
        // }
        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Property Configurations 
            builder.ToTable("Users");
           
            // Populate the table Users
            var hmac = new HMACSHA512();
            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Javier",
                    LastName = "Vazquez",
                    Email = "jv@admin.com",
                    Password = "PxeMXgybOIe",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/jv.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("PxeMX(gybOIe")),
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Macarena",
                    LastName = "Zalazar",
                    Email = "mz@admin.com",
                    Password = "RU)yAPiGb1V_",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/mz.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("RU)yAPiGb1V_")),
                    RoleId = 1
                },
                new User
                {
                    Id = 3,
                    FirstName = "Julian",
                    LastName = "Hernandez",
                    Email = "jh@admin.com",
                    Password = "vFJuqc#5a*h!",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/jh.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("vFJuqc#5a*h!")),
                    RoleId = 1
                },
                new User
                {
                    Id = 4,
                    FirstName = "Horacio",
                    LastName = "Musser",
                    Email = "hm@admin.com",
                    Password = "GXoe2lW)2WTB",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/hm.jpeg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("GXoe2lW)2WTB")),
                    RoleId = 1
                },
                new User
                {
                    Id = 5,
                    FirstName = "Guillermo",
                    LastName = "Donalisio",
                    Email = "gd@admin.com",
                    Password = "ZJYBt1MlY6Dp",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/gd.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("ZJYBt1MlY6Dp")),
                    RoleId = 1
                },
                new User
                {
                    Id = 6,
                    FirstName = "Fernando",
                    LastName = "Percussio",
                    Email = "fernandoPercussio@owner.com",
                    Password = "dOi-I?o9#ckR",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/fp.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("dOi-I?o9#ckR")),
                    RoleId = 2
                },
                new User
                {
                    Id = 7,
                    FirstName = "Victoria",
                    LastName = "Fernandez",
                    Email = "vickyFern@owner.com",
                    Password = "!u?28*18?YkU",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/vf.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("!u?28*18?YkU")),
                    RoleId = 2
                },
                new User
                {
                    Id = 8,
                    FirstName = "Veronica",
                    LastName = "Fisher",
                    Email = "verofish@owner.com",
                    Password = "7iVMd4Gwv6oI",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/vf.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("7iVMd4Gwv6oI")),
                    RoleId = 2
                },
                new User
                {
                    Id = 9,
                    FirstName = "Micaela",
                    LastName = "Losano",
                    Email = "micaelalosano@owner.com",
                    Password = "E:b4vEdsttg*",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/ml.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("E:b4vEdsttg*")),
                    RoleId = 2
                },
                new User
                {
                    Id = 10,
                    FirstName = "Facundo",
                    LastName = "Noriega",
                    Email = "facundoNor1@owner.com",
                    Password = "GTXX*eVRrtsg",
                    Photo = "https://cohorte-mayo-2820e45d.s3.amazonaws.com/fn.jpg",
                    PasswordHash = hmac.Key,
                    PasswordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes("GTXX*eVRrtsg")),
                    RoleId = 2
                }

            );
        }
    }

}

