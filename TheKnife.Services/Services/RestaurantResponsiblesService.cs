using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantResponsiblesService
    {
        Task<List<RestaurantResponsiblesEfo>> GetAllRestaurantResponsibles();
        Task<RestaurantResponsiblesEfo> GetRestaurantResponsibleById(int id);
        Task<RestaurantResponsiblesEfo> SendRestaurantResponsible(RestaurantResponsiblesEfo restaurantResponsible);
        Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsiblePassword(string email, string newPassword, string confirmPassword);
        Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsible(int id, RestaurantResponsiblesEfo updateResponsible);
        Task DeleteRestaurantResponsible(int id);
    }

    public class RestaurantResponsiblesService : IRestaurantResponsiblesService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantResponsiblesService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantResponsiblesEfo>> GetAllRestaurantResponsibles()
        {
            return await _context.RestaurantResponsibles.ToListAsync();
        }

        public async Task<RestaurantResponsiblesEfo> GetRestaurantResponsibleById(int id)
        {
            RestaurantResponsiblesEfo restaurantResponsible = await _context.RestaurantResponsibles
                .AsNoTracking().FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantResponsible == null)
            {
                throw new Exception("Responsável do restaurante não encontrado");
            }

            return restaurantResponsible;
        }

        public async Task<RestaurantResponsiblesEfo> SendRestaurantResponsible(RestaurantResponsiblesEfo restaurantResponsible)
        {
            try
            {
                restaurantResponsible.Password = HashPassword(restaurantResponsible.Password);

                await _context.RestaurantResponsibles.AddAsync(restaurantResponsible);
                await _context.SaveChangesAsync();

                return restaurantResponsible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar registo de utilizador: {ex.Message}");
            }
        }

        public async Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsiblePassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                RestaurantResponsiblesEfo restaurantResponsible = await _context.RestaurantResponsibles.FirstOrDefaultAsync(
                    ru => ru.Email == email);

                if (restaurantResponsible == null)
                {
                    throw new Exception("Responsável não encontrado");
                }

                if (newPassword != confirmPassword)
                {
                    throw new Exception("As passwords devem ser iguais");
                }

                string hashedPassword = HashPassword(newPassword);

                restaurantResponsible.Password = hashedPassword;
                await _context.SaveChangesAsync();

                return restaurantResponsible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar password: {ex.Message}");
            }
        }

        public async Task<RestaurantResponsiblesEfo> UpdateRestaurantResponsible(int id, RestaurantResponsiblesEfo updateResponsible)
        {
            try
            {
                RestaurantResponsiblesEfo newResponsible = await _context.RestaurantResponsibles
                    .FirstOrDefaultAsync(rr => rr.Id == id);

                if (newResponsible == null)
                {
                    throw new Exception("Responsável não encontrado");
                }

                string hashedPassword = HashPassword(updateResponsible.Password);

                newResponsible.FlName = updateResponsible.FlName;
                newResponsible.Phone = updateResponsible.Phone;
                newResponsible.Email = updateResponsible.Email;
                newResponsible.Password = hashedPassword;
                newResponsible.RImage = updateResponsible.RImage;

                await _context.SaveChangesAsync();

                return newResponsible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do responsável: {ex.Message}");
            }
        }

        public async Task DeleteRestaurantResponsible(int id)
        {
            RestaurantResponsiblesEfo restaurantResponsible = await _context.RestaurantResponsibles
                .FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantResponsible == null)
            {
                throw new Exception("Responsável não encontrado");
            }

            _context.RestaurantResponsibles.Remove(restaurantResponsible);
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
