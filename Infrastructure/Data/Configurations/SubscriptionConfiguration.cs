using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CompanyId).HasColumnName("companyid");
        builder.Property(x => x.PlanType).HasColumnName("plantype").IsRequired();
        builder.Property(x => x.MealsPerDay).HasColumnName("mealsperday");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.StartDate).HasColumnName("startdate").HasColumnType("date");
        builder.Property(x => x.EndDate).HasColumnName("enddate").HasColumnType("date");
        builder.Property(x => x.IsActive).HasColumnName("isactive");
        builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
