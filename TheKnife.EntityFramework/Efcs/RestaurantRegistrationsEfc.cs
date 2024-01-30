using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class RestaurantRegistrationsEfc : IEntityTypeConfiguration<RestaurantRegistrationsEfo>
    {
       public void Configure(EntityTypeBuilder<RestaurantRegistrationsEfo> builder)
       {
            builder.ToTable("RestaurantRegistrations");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.FlName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Phone).IsRequired();
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
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
       }
    }
}
