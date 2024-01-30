using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantRegistrationsService
    {
        Task<List<RestaurantRegistrationsEfo>> GetAllRestaurantRegistrations();
        Task<RestaurantRegistrationsEfo> GetRestaurantRegistrationById(int id);
        Task<RestaurantRegistrationsEfo> SendRestaurantRegistration(RestaurantRegistrationsEfo restaurantRegistration);
        Task<RestaurantRegistrationsEfo> UpdateRestaurantRegistration(int id, RestaurantRegistrationsEfo updateRestaurantRegistration);
        Task DeleteRestaurantRegistration(int id);
    }

    public class RestaurantRegistrationsService : IRestaurantRegistrationsService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantRegistrationsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantRegistrationsEfo>> GetAllRestaurantRegistrations()
        {
            return await _context.RestaurantRegistrations.ToListAsync();
        }

        public async Task<RestaurantRegistrationsEfo> GetRestaurantRegistrationById(int id)
        {
            RestaurantRegistrationsEfo restaurantRegistration = await _context.RestaurantRegistrations
                .AsNoTracking().FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantRegistration == null)
            {
                throw new Exception("Registo de restaurnate não encontrado");
            }

            return restaurantRegistration;
        }

        public async Task<RestaurantRegistrationsEfo> SendRestaurantRegistration(RestaurantRegistrationsEfo restaurantRegistration)
        {
            try
            {
                restaurantRegistration.Password = HashPassword(restaurantRegistration.Password);

                await _context.RestaurantRegistrations.AddAsync(restaurantRegistration);
                await _context.SaveChangesAsync();

                return restaurantRegistration;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar registo de restaurante: {ex.Message}");
            }
        }

        public async Task<RestaurantRegistrationsEfo> UpdateRestaurantRegistration(int id, RestaurantRegistrationsEfo updateRestaurantRegistration)
        {
            try
            {
                RestaurantRegistrationsEfo newRestaurantRegistration = await _context.RestaurantRegistrations
                    .FirstOrDefaultAsync(rr => rr.Id == id);

                if (newRestaurantRegistration == null)
                {
                    throw new Exception("Registo de restaurante não encontrado");
                }

                string hashedPassword = HashPassword(updateRestaurantRegistration.Password);

                newRestaurantRegistration.FlName = updateRestaurantRegistration.FlName;
                newRestaurantRegistration.Phone = updateRestaurantRegistration.Phone;
                newRestaurantRegistration.Email = updateRestaurantRegistration.Email;
                newRestaurantRegistration.Password = hashedPassword;
                newRestaurantRegistration.RName = updateRestaurantRegistration.RName;
                newRestaurantRegistration.Category = updateRestaurantRegistration.Category;
                newRestaurantRegistration.Desc = updateRestaurantRegistration.Desc;
                newRestaurantRegistration.RPhone = updateRestaurantRegistration.RPhone;
                newRestaurantRegistration.Location = updateRestaurantRegistration.Location;
                newRestaurantRegistration.Image = updateRestaurantRegistration.Image;
                newRestaurantRegistration.NumberOfTables = updateRestaurantRegistration.NumberOfTables;
                newRestaurantRegistration.Capacity = updateRestaurantRegistration.Capacity;
                newRestaurantRegistration.OpeningDays = updateRestaurantRegistration.OpeningDays;
                newRestaurantRegistration.AveragePrice = updateRestaurantRegistration.AveragePrice;
                newRestaurantRegistration.OpeningHours = updateRestaurantRegistration.OpeningHours;
                newRestaurantRegistration.ClosingHours = updateRestaurantRegistration.ClosingHours;

                await _context.SaveChangesAsync();

                return newRestaurantRegistration;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do restaurante: {ex.Message}");
            }
        }

        public async Task DeleteRestaurantRegistration(int id)
        {
            RestaurantRegistrationsEfo restaurantRegistration = await _context.RestaurantRegistrations
                .FirstOrDefaultAsync(rr => rr.Id == id);

            if (restaurantRegistration == null )
            {
                throw new Exception("Registo de restaurante não encontrado");
            }

            _context.Remove(restaurantRegistration);
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
