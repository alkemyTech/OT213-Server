using System;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<User> Login(string email, string pass)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            var verify = VerifyPasswordHash(pass, user.PasswordHash, user.PasswordSalt);
            if (!verify)
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string pass, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != computeHash[i]) return false;
                }
            }
            return true;
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

            if (user.Email.Contains("admin"))
            {
                user.RoleId = 1;
            }
            else
            {
                user.RoleId = 2;
            }

            await _context.Users.AddAsync(user);

            return user;
        }

        private void CreatePasswordHash(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.Key;
                passwordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));
            }
        }
        #endregion
    }

}
