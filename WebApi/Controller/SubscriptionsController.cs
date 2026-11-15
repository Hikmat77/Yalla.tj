using Application.DTOs.subscriptions;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController(
    ISubscriptionService subscriptionService,
    ILogger<SubscriptionsController> logger) : ControllerBase
{
    private ISubscriptionService service = subscriptionService;
    private ILogger<SubscriptionsController> _logger = logger;

    [HttpGet("company/{companyId:int}")]
    public async Task<ApiResponse<List<SubscriptionDto>>> GetCompanySubscriptions(int companyId)
    {
        _logger.LogInformation("GET api/Subscriptions/company/{companyId} {date}", companyId, DateTime.Now);
        var res = await service.GetCompanySubscriptionsAsync(companyId);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<string>> AddSubscription(CreateSubscriptionDto request)
    {
        _logger.LogInformation("POST api/Subscriptions {date}", DateTime.Now);
        var res = await service.CreateSubscriptionAsync(request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPut("{id:int}/status")]
    public async Task<ApiResponse<string>> UpdateSubscriptionStatus(int id, UpdateSubscriptionStatusDto request)
    {
        _logger.LogInformation("PUT api/Subscriptions/{id}/status {date}", id, DateTime.Now);
        var res = await service.UpdateSubscriptionStatusAsync(id, request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("active")]
    public async Task<ApiResponse<List<SubscriptionDto>>> GetActiveSubscriptions()
    {
        _logger.LogInformation("GET api/Subscriptions/active {date}", DateTime.Now);
        var res = await service.GetActiveSubscriptionsAsync();
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }
}