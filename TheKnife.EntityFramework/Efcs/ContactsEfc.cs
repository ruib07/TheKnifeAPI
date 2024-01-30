using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class ContactsEfc : IEntityTypeConfiguration<ContactsEfo>
    {
        public void Configure(EntityTypeBuilder<ContactsEfo> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(p =>new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.ClientName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Subject).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Message).IsRequired().HasMaxLength(250);
        }
    }
}
