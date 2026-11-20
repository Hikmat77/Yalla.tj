using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderitems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.OrderId).HasColumnName("orderid");
        builder.Property(x => x.MenuItemId).HasColumnName("menuitemid");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MenuItem)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.MenuItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
