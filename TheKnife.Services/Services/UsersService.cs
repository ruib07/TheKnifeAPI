using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IUsersService
    {
        Task<List<UsersEfo>> GetAllUsers();
        Task<UsersEfo> GetUserById(int id);
        Task<UsersEfo> SendUser(UsersEfo user);
        Task<UsersEfo> UpdateUser(int id, UsersEfo updateUser);
        Task DeleteUser(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly TheKnifeDbContext _context;

        public UsersService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsersEfo>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UsersEfo> GetUserById(int id)
        {
            UsersEfo user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Utilizador não encontrado");
            }

            return user;
        }

        public async Task<UsersEfo> SendUser(UsersEfo user)
        {
            try
            {
                user.Password = HashPassword(user.Password);

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar utilizador: {ex.Message}");
            }
        }

        public async Task<UsersEfo> UpdateUser(int id, UsersEfo updateUser)
        {
            try
            {
                UsersEfo newUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (newUser == null)
                {
                    throw new Exception("Utilizador não encontrado");
                }

                string hashedPassword = HashPassword(updateUser.Password);

                newUser.UserName = updateUser.UserName;
                newUser.Email = updateUser.Email;
                newUser.Password = hashedPassword;
                newUser.Image = updateUser.Image;

                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do utilizador: {ex.Message}");
            }
        }

        public async Task DeleteUser(int id)
        {
            UsersEfo user = await _context.Users.FirstOrDefaultAsync(
                u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Utilizador não encontrado");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32);

            byte[] hashBytes = new byte[16 + 32];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
