using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Auth.Interfaces;

namespace OngProject.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private OngProjectDbContext _context;
        public AuthRepository(OngProjectDbContext context)
        {
            this._context = context;
        }

        #region Validate User 
        public async Task<bool> ExistsUser(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email == email))
                return true;

            return false;
        }
        #endregion

        #region Login User 
        // Wrie here the login code...
        #endregion 

        #region Register User 
        public async Task<User> Registrar(User user, string pass)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(pass, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(user);

            return user;
        }

        private void CreatePasswordHash(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.Key;
                passwordSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            }
        }
        #endregion
    }

}

