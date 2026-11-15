using System.Net;
using Application.DTOs.orders;
using Application.Interfaces;
using Application.Responses;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICompanyRepository companyRepository,
    IMenuRepository menuRepository) : IOrderService
{
    private IOrderRepository repository = orderRepository;
    private ICompanyRepository _companyRepository = companyRepository;
    private IMenuRepository _menuRepository = menuRepository;

    public async Task<ApiResponse<List<OrderDto>>> GetCompanyOrdersAsync(int companyId)
    {
        var company = await _companyRepository.GetCompanyAsync(companyId);
        if (company == null)
        {
            return new ApiResponse<List<OrderDto>>(HttpStatusCode.NotFound, "Company not found.");
        }
        var orders = await repository.GetCompanyOrdersAsync(companyId);
        var items = await repository.GetOrderItemsAsync(orders.Select(x => x.Id).ToList());
        var res = orders.Select(x => new OrderDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            OrderDate = x.OrderDate,
            Status = x.Status,
            TotalAmount = x.TotalAmount,
            Items = items.Where(i => i.OrderId == x.Id).Select(i => new OrderItemDto()
            {
                Id = i.Id,
                MenuItemId = i.MenuItemId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
        return new ApiResponse<List<OrderDto>>(HttpStatusCode.OK, "List of orders of the company.", res);
    }

    public async Task<ApiResponse<string>> CreateOrderAsync(CreateOrderDto request)
    {
        if (request.Items.Count == 0)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Order must contain at least one item.");
        }
        var company = await _companyRepository.GetCompanyAsync(request.CompanyId);
        if (company == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Company not found.");
        }
        var ids = request.Items.Select(x => x.MenuItemId).Distinct().ToList();
        var menuItems = await _menuRepository.GetMenuItemsByIdsAsync(ids);
        if (menuItems.Count != ids.Count)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Some menu items were not found.");
        }
        var items = request.Items.Select(x => new OrderItem()
        {
            MenuItemId = x.MenuItemId,
            Quantity = x.Quantity,
            Price = menuItems.First(m => m.Id == x.MenuItemId).Price
        }).ToList();
        var order = new Order()
        {
            CompanyId = request.CompanyId,
            OrderDate = DateTime.SpecifyKind(request.OrderDate.Date, DateTimeKind.Unspecified),
            Status = OrderStatus.Pending.ToString(),
            TotalAmount = items.Sum(x => x.Price * x.Quantity)
        };
        var res = await repository.AddOrderAsync(order, items);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.Created, "Added!")
        : new ApiResponse<string>(HttpStatusCode.InternalServerError, "Not added.");
    }

    public async Task<ApiResponse<string>> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto request)
    {
        var status = Enum.GetNames<OrderStatus>().FirstOrDefault(x => x.Equals(request.Status, StringComparison.OrdinalIgnoreCase));
        if (status == null)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Invalid status. Allowed: " + string.Join(", ", Enum.GetNames<OrderStatus>()));
        }
        var res = await repository.UpdateOrderStatusAsync(id, status);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.OK, "Updated!")
        : new ApiResponse<string>(HttpStatusCode.NotFound, "Order not found.");
    }

    public async Task<ApiResponse<DailySummaryDto>> GetDailySummaryAsync(DateTime date)
    {
        var summary = await repository.GetDailySummaryAsync(date);
        var res = new DailySummaryDto()
        {
            Date = date.Date,
            TotalOrders = summary.TotalOrders,
            TotalItems = summary.TotalItems,
            TotalAmount = summary.TotalAmount
        };
        return new ApiResponse<DailySummaryDto>(HttpStatusCode.OK, "Summary of orders for the day.", res);
    }

    public async Task<ApiResponse<List<CompanyStatisticsDto>>> GetCompanyOrderStatisticsAsync()
    {
        var statistics = await repository.GetCompanyStatisticsAsync();
        var res = statistics.Select(x => new CompanyStatisticsDto()
        {
            CompanyId = x.CompanyId,
            CompanyName = x.CompanyName,
            TotalOrders = x.TotalOrders,
            TotalAmount = x.TotalAmount,
            LastOrderDate = x.LastOrderDate
        }).ToList();
        return new ApiResponse<List<CompanyStatisticsDto>>(HttpStatusCode.OK, "Order statistics of companies.", res);
    }
}