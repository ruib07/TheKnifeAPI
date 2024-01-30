using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRegisterUsersService
    {
        Task<List<RegisterUsersEfo>> GetAllRegisterUsers();
        Task<RegisterUsersEfo> GetRegisterUserById(int id);
        Task<RegisterUsersEfo> SendRegisterUser(RegisterUsersEfo registerUser);
        Task<RegisterUsersEfo> UpdateUserPassword(string email, string newPassword, string confirmPassword);
        Task<RegisterUsersEfo> UpdateRegisterUser(int id, RegisterUsersEfo updateRegisterUser);
        Task DeleteRegisterUser(int id);
    }

    public class RegisterUsersService : IRegisterUsersService
    {
        private readonly TheKnifeDbContext _context;

        public RegisterUsersService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegisterUsersEfo>> GetAllRegisterUsers()
        {
            return await _context.RegisterUsers.ToListAsync();
        }

        public async Task<RegisterUsersEfo> GetRegisterUserById(int id)
        {
            RegisterUsersEfo registerUser = await _context.RegisterUsers.AsNoTracking()
                .FirstOrDefaultAsync(ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Registo de Utilizador não encontrado!");
            }

            return registerUser;
        }

        public async Task<RegisterUsersEfo> SendRegisterUser(RegisterUsersEfo registerUser)
        {
            try
            {
                registerUser.Password = HashPassword(registerUser.Password);

                await _context.RegisterUsers.AddAsync(registerUser);
                await _context.SaveChangesAsync();

                return registerUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar registo de utilizador: {ex.Message}");
            }
        }

        public async Task<RegisterUsersEfo> UpdateUserPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                RegisterUsersEfo registerUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                    ru => ru.Email == email);

                if (registerUser == null)
                {
                    throw new Exception("Registo de utilizador não encontrado");
                }

                if (newPassword != confirmPassword)
                {
                    throw new Exception("As passwords devem ser iguais");
                }

                string hashedPassword = HashPassword(newPassword);

                registerUser.Password = hashedPassword;

                await _context.SaveChangesAsync();

                return registerUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar password: {ex.Message}");
            }
        }

        public async Task<RegisterUsersEfo> UpdateRegisterUser(int id, RegisterUsersEfo updateRegisterUser)
        {
            try
            {
                RegisterUsersEfo newRegisterUser = await _context.RegisterUsers.FirstOrDefaultAsync(u => u.Id == id);

                if (newRegisterUser == null)
                {
                    throw new Exception("Registo de utilizador não encontrado");
                }

                string hashedPassword = HashPassword(updateRegisterUser.Password);

                newRegisterUser.UserName = updateRegisterUser.UserName;
                newRegisterUser.Email = updateRegisterUser.Email;
                newRegisterUser.Password = hashedPassword;

                await _context.SaveChangesAsync();

                return newRegisterUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do utilizador: {ex.Message}");
            }
        }

        public async Task DeleteRegisterUser(int id)
        {
            RegisterUsersEfo registerUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Registo de utilizador não encontrado");
            }

            _context.RegisterUsers.Remove(registerUser);
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
