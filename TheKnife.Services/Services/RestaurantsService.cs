using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IRestaurantsService
    {
        Task<List<RestaurantsEfo>> GetAllRestaurants();
        Task<RestaurantsEfo> GetRestaurantById(int id);
        Task<RestaurantsEfo> SendRestaurant(RestaurantsEfo restaurant);
        Task<RestaurantsEfo> UpdateRestaurant(int id, RestaurantsEfo updateRestaurant);
        Task DeleteRestaurant(int id);
    }

    public class RestaurantsService : IRestaurantsService
    {
        private readonly TheKnifeDbContext _context;

        public RestaurantsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantsEfo>> GetAllRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<RestaurantsEfo> GetRestaurantById(int id)
        {
            RestaurantsEfo restaurant = await _context.Restaurants.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                throw new Exception("Restaurante não encontrado");
            }

            return restaurant;
        }

        public async Task<RestaurantsEfo> SendRestaurant(RestaurantsEfo restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }

        public async Task<RestaurantsEfo> UpdateRestaurant(int id, RestaurantsEfo updateRestaurant)
        {
            try
            {
                RestaurantsEfo newRestaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (newRestaurant == null)
                {
                    throw new Exception("Restaurante não encontrado");
                }

                newRestaurant.RName = updateRestaurant.RName;
                newRestaurant.Category = updateRestaurant.Category;
                newRestaurant.Desc = updateRestaurant.Desc;
                newRestaurant.RPhone = updateRestaurant.RPhone;
                newRestaurant.Location = updateRestaurant.Location;
                newRestaurant.Image = updateRestaurant.Image;
                newRestaurant.NumberOfTables = updateRestaurant.NumberOfTables;
                newRestaurant.Capacity = updateRestaurant.Capacity;
                newRestaurant.OpeningDays = updateRestaurant.OpeningDays;
                newRestaurant.AveragePrice = updateRestaurant.AveragePrice;
                newRestaurant.OpeningHours = updateRestaurant.OpeningHours;
                newRestaurant.ClosingHours = updateRestaurant.ClosingHours;

                await _context.SaveChangesAsync();

                return newRestaurant;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do restaurante: {ex.Message}");
            }
        }

        public async Task DeleteRestaurant(int id)
        {
            RestaurantsEfo restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                throw new Exception("Restaurante não encontrado");
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
