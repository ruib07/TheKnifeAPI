using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class RestaurantsEfc : IEntityTypeConfiguration<RestaurantsEfo>
    {
        public void Configure(EntityTypeBuilder<RestaurantsEfo> builder)
        {
            builder.ToTable("Restaurants");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.RName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Category).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Desc).IsRequired().HasMaxLength(250);
            builder.Property(p => p.RPhone).IsRequired();
            builder.Property(p => p.Location).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Image).IsRequired().HasMaxLength(400);
            builder.Property(p => p.NumberOfTables).IsRequired();
            builder.Property(p => p.Capacity).IsRequired();
            builder.Property(p => p.OpeningDays).IsRequired().HasMaxLength(100);
            builder.Property(p => p.AveragePrice).IsRequired().HasColumnType("decimal(5, 1)");
            builder.Property(p => p.OpeningHours).IsRequired().HasMaxLength(5);
            builder.Property(p => p.ClosingHours).IsRequired().HasMaxLength(5);
            builder.Property(p => p.RestaurantRegistration_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Rresponsible_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.RestaurantRegistrations)
                .WithOne()
                .HasForeignKey<RestaurantsEfo>(p => p.RestaurantRegistration_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.RestaurantResponsibles)
                .WithOne()
                .HasForeignKey<RestaurantsEfo>(p => p.Rresponsible_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
