using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class ReservationsEfc : IEntityTypeConfiguration<ReservationsEfo>
    {
        public void Configure(EntityTypeBuilder<ReservationsEfo> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.ClientName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.ReservationDate).IsRequired();
            builder.Property(p => p.ReservationTime).IsRequired().HasMaxLength(5);
            builder.Property(p => p.NumberPeople).IsRequired();
            builder.Property(p => p.Restaurant_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.User_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.Restaurants)
                .WithMany()
                .HasForeignKey(p => p.Restaurant_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Users)
                .WithMany()
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
