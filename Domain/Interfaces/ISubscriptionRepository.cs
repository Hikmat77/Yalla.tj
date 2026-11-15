using Domain.Models;

namespace Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<bool> AddSubscriptionAsync(Subscription subscription);
    Task<bool> UpdateSubscriptionStatusAsync(int id, bool isActive);
    Task<List<Subscription>> GetCompanySubscriptionsAsync(int companyId);
    Task<List<Subscription>> GetActiveSubscriptionsAsync();
}