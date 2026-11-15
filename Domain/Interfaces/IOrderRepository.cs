using Domain.Models;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<bool> AddOrderAsync(Order order, List<OrderItem> items);
    Task<bool> UpdateOrderStatusAsync(int id, string status);
    Task<List<Order>> GetCompanyOrdersAsync(int companyId);
    Task<List<OrderItem>> GetOrderItemsAsync(List<int> orderIds);
    Task<DailySummary> GetDailySummaryAsync(DateTime date);
    Task<List<CompanyStatistics>> GetCompanyStatisticsAsync();
}
