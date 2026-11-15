using System.Net;
using Application.DTOs.subscriptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SubscriptionService(
    ISubscriptionRepository subscriptionRepository,
    ICompanyRepository companyRepository) : ISubscriptionService
{
    private ISubscriptionRepository repository = subscriptionRepository;
    private ICompanyRepository _companyRepository = companyRepository;

    public async Task<ApiResponse<List<SubscriptionDto>>> GetCompanySubscriptionsAsync(int companyId)
    {
        var company = await _companyRepository.GetCompanyAsync(companyId);
        if (company == null)
        {
            return new ApiResponse<List<SubscriptionDto>>(HttpStatusCode.NotFound, "Company not found.");
        }
        var subscriptions = await repository.GetCompanySubscriptionsAsync(companyId);
        var res = subscriptions.Select(ToDto).ToList();
        return new ApiResponse<List<SubscriptionDto>>(HttpStatusCode.OK, "List of subscriptions of the company.", res);
    }

    public async Task<ApiResponse<string>> CreateSubscriptionAsync(CreateSubscriptionDto request)
    {
        if (request.EndDate < request.StartDate)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "EndDate cannot be earlier than StartDate.");
        }
        var company = await _companyRepository.GetCompanyAsync(request.CompanyId);
        if (company == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Company not found.");
        }
        var subscription = new Subscription()
        {
            CompanyId = request.CompanyId,
            PlanType = request.PlanType,
            MealsPerDay = request.MealsPerDay,
            Price = request.Price,
            StartDate = DateTime.SpecifyKind(request.StartDate.Date, DateTimeKind.Unspecified),
            EndDate = DateTime.SpecifyKind(request.EndDate.Date, DateTimeKind.Unspecified),
            IsActive = true
        };
        var res = await repository.AddSubscriptionAsync(subscription);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.Created, "Added!")
        : new ApiResponse<string>(HttpStatusCode.InternalServerError, "Not added.");
    }

    public async Task<ApiResponse<string>> UpdateSubscriptionStatusAsync(int id, UpdateSubscriptionStatusDto request)
    {
        var res = await repository.UpdateSubscriptionStatusAsync(id, request.IsActive);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.OK, "Updated!")
        : new ApiResponse<string>(HttpStatusCode.NotFound, "Subscription not found.");
    }

    public async Task<ApiResponse<List<SubscriptionDto>>> GetActiveSubscriptionsAsync()
    {
        var subscriptions = await repository.GetActiveSubscriptionsAsync();
        var res = subscriptions.Select(ToDto).ToList();
        return new ApiResponse<List<SubscriptionDto>>(HttpStatusCode.OK, "List of active subscriptions.", res);
    }

    private SubscriptionDto ToDto(Subscription x)
    {
        return new SubscriptionDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            PlanType = x.PlanType,
            MealsPerDay = x.MealsPerDay,
            Price = x.Price,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            IsActive = x.IsActive
        };
    }
}