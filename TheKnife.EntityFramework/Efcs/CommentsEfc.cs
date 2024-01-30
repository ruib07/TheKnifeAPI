using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheKnife.Entities.Efos;

namespace TheKnife.EntityFramework.Efcs
{
    public class CommentsEfc : IEntityTypeConfiguration<CommentsEfo>
    {
        public void Configure(EntityTypeBuilder<CommentsEfo> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(p => new { p.Id });
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.CommentDate).IsRequired();
            builder.Property(p => p.Review).IsRequired().HasColumnType("decimal(3, 1)");
            builder.Property(p => p.Comment).IsRequired().HasMaxLength(250);
            builder.Property(p => p.User_Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Restaurant_Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.Users)
                .WithMany()
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Restaurants)
                .WithMany()
                .HasForeignKey(p => p.Restaurant_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
