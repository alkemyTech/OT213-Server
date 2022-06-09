using System;
using System.Security.Cryptography;
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
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return true;

            return false;
        }
        #endregion

        #region Login User 
        // Wrie here the login code...
        public async Task<bool> Login(User user, string pass)
        {

            if (!VerifyPasswordHash(user.Password, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            return true;
        }
        private bool VerifyPasswordHash(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            return computedHash.SequenceEqual(passwordHash);
        }
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
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

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

