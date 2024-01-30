using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class RegisterUsersEfc : IEntityTypeConfiguration<RegisterUsersEfo>
    {
        public void Configure(EntityTypeBuilder<RegisterUsersEfo> builder)
        {
            builder.ToTable("RegisterUsers");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(100);
        }
    }
}
