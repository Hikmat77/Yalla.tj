using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("menus");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.MenuDate).HasColumnName("menudate").HasColumnType("date");
        builder.Property(x => x.IsActive).HasColumnName("isactive");
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
