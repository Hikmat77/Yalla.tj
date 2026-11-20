using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CompanyId).HasColumnName("companyid");
        builder.Property(x => x.OrderDate).HasColumnName("orderdate").HasColumnType("date");
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnName("totalamount");
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
