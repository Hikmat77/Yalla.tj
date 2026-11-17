using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SubscriptionRepository(AppDbContext appDbContext) : ISubscriptionRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddSubscriptionAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<List<Subscription>> GetActiveSubscriptionsAsync()
    {
        return _context.Subscriptions
            .Where(x => x.IsActive)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public Task<List<Subscription>> GetCompanySubscriptionsAsync(int companyId)
    {
        return _context.Subscriptions
            .Where(x => x.CompanyId == companyId)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<bool> UpdateSubscriptionStatusAsync(int id, bool isActive)
    {
        var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
        if (subscription is null)
        {
            return false;
        }

        subscription.IsActive = isActive;
        subscription.UpdatedAt = DateTime.UtcNow;

        return await _context.SaveChangesAsync() > 0;
    }
}
