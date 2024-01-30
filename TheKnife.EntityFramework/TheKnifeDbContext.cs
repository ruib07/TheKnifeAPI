using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework.Efcs;

namespace TheKnife.EntityFramework
{
    public class TheKnifeDbContext : DbContext
    {
        public TheKnifeDbContext(DbContextOptions<TheKnifeDbContext> options) : base(options) { }

        public DbSet<RegisterUsersEfo> RegisterUsers {  get; set; }
        public DbSet<UsersEfo> Users { get; set; }
        public DbSet<RestaurantRegistrationsEfo> RestaurantRegistrations { get; set; }
        public DbSet<RestaurantResponsiblesEfo> RestaurantResponsibles { get; set; }
        public DbSet<RestaurantsEfo> Restaurants {  get; set; }
        public DbSet<ContactsEfo> Contacts { get; set; }
        public DbSet<ReservationsEfo> Reservations { get; set; }
        public DbSet<CommentsEfo> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterUsersEfc());
            modelBuilder.ApplyConfiguration(new UsersEfc());
            modelBuilder.ApplyConfiguration(new RestaurantRegistrationsEfc());
            modelBuilder.ApplyConfiguration(new RestaurantResponsiblesEfc());
            modelBuilder.ApplyConfiguration(new RestaurantsEfc());
            modelBuilder.ApplyConfiguration(new ContactsEfc());
            modelBuilder.ApplyConfiguration(new ReservationsEfc());
            modelBuilder.ApplyConfiguration(new CommentsEfc());
        }
    }
}
