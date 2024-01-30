using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class RestaurantResponsiblesEfc : IEntityTypeConfiguration<RestaurantResponsiblesEfo>
    {
        public void Configure(EntityTypeBuilder<RestaurantResponsiblesEfo> builder)
        {
            builder.ToTable("RestaurantResponsibles");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.FlName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Phone).IsRequired();
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
            builder.Property(p => p.RImage).HasMaxLength(400);
            builder.Property(p => p.RestaurantRegistration_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.RestaurantRegistrations)
                .WithOne()
                .HasForeignKey<RestaurantResponsiblesEfo>(p => p.RestaurantRegistration_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
