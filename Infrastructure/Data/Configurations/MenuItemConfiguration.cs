using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("menuitems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.MenuId).HasColumnName("menuid");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.Category).HasColumnName("category").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(x => x.Menu)
            .WithMany(x => x.MenuItems)
            .HasForeignKey(x => x.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
