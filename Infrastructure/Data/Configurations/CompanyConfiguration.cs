using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Address).HasColumnName("address").IsRequired();
        builder.Property(x => x.Phone).HasColumnName("phone").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
