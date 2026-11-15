using Application.DTOs.orders;
using Application.Responses;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<ApiResponse<List<OrderDto>>> GetCompanyOrdersAsync(int companyId);
    Task<ApiResponse<string>> CreateOrderAsync(CreateOrderDto request);
    Task<ApiResponse<string>> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto request);
    Task<ApiResponse<DailySummaryDto>> GetDailySummaryAsync(DateTime date);
    Task<ApiResponse<List<CompanyStatisticsDto>>> GetCompanyOrderStatisticsAsync();
}