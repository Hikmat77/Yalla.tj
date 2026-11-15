using Application.DTOs.orders;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    IOrderService orderService,
    ILogger<OrdersController> logger) : ControllerBase
{
    private IOrderService service = orderService;
    private ILogger<OrdersController> _logger = logger;

    [HttpGet("company/{companyId:int}")]
    public async Task<ApiResponse<List<OrderDto>>> GetCompanyOrders(int companyId)
    {
        _logger.LogInformation("GET api/Orders/company/{companyId} {date}", companyId, DateTime.Now);
        var res = await service.GetCompanyOrdersAsync(companyId);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<string>> AddOrder(CreateOrderDto request)
    {
        _logger.LogInformation("POST api/Orders {date}", DateTime.Now);
        var res = await service.CreateOrderAsync(request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPut("{id:int}/status")]
    public async Task<ApiResponse<string>> UpdateOrderStatus(int id, UpdateOrderStatusDto request)
    {
        _logger.LogInformation("PUT api/Orders/{id}/status {date}", id, DateTime.Now);
        var res = await service.UpdateOrderStatusAsync(id, request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("daily-summary")]
    public async Task<ApiResponse<DailySummaryDto>> GetDailySummary(DateTime date)
    {
        _logger.LogInformation("GET api/Orders/daily-summary?date={date} {now}", date, DateTime.Now);
        var res = await service.GetDailySummaryAsync(date);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("company-statistics")]
    public async Task<ApiResponse<List<CompanyStatisticsDto>>> GetCompanyOrderStatistics()
    {
        _logger.LogInformation("GET api/Orders/company-statistics {date}", DateTime.Now);
        var res = await service.GetCompanyOrderStatisticsAsync();
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }
}