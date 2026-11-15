using Application.DTOs.subscriptions;
using Application.Responses;

namespace Application.Interfaces;

public interface ISubscriptionService
{
    Task<ApiResponse<List<SubscriptionDto>>> GetCompanySubscriptionsAsync(int companyId);
    Task<ApiResponse<string>> CreateSubscriptionAsync(CreateSubscriptionDto request);
    Task<ApiResponse<string>> UpdateSubscriptionStatusAsync(int id, UpdateSubscriptionStatusDto request);
    Task<ApiResponse<List<SubscriptionDto>>> GetActiveSubscriptionsAsync();
}