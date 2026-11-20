using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
