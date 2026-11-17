using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddOrderAsync(Order order, List<OrderItem> items)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            item.OrderId = order.Id;
        }

        await _context.OrderItems.AddRangeAsync(items);
        var result = await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return result > 0;
    }

    public async Task<List<CompanyStatistics>> GetCompanyStatisticsAsync()
    {
        return await (
            from company in _context.Companies
            join order in _context.Orders on company.Id equals order.CompanyId into companyOrders
            select new CompanyStatistics
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                TotalOrders = companyOrders.Count(),
                TotalAmount = companyOrders.Sum(x => (decimal?)x.TotalAmount) ?? 0,
                LastOrderDate = companyOrders.Max(x => (DateTime?)x.OrderDate)
            }).ToListAsync();
    }

    public Task<List<Order>> GetCompanyOrdersAsync(int companyId)
    {
        return _context.Orders
            .Where(x => x.CompanyId == companyId)
            .OrderByDescending(x => x.OrderDate)
            .ThenByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<DailySummary> GetDailySummaryAsync(DateTime date)
    {
        var dailyOrders = _context.Orders.Where(x => x.OrderDate == date.Date);
        var orderSummary = await dailyOrders
            .GroupBy(x => x.OrderDate)
            .Select(group => new DailySummary
            {
                TotalOrders = group.Count(),
                TotalAmount = group.Sum(x => x.TotalAmount)
            })
            .FirstOrDefaultAsync();

        var totalItems = await (
            from item in _context.OrderItems
            join order in dailyOrders on item.OrderId equals order.Id
            select (int?)item.Quantity).SumAsync() ?? 0;

        return new DailySummary
        {
            TotalOrders = orderSummary?.TotalOrders ?? 0,
            TotalAmount = orderSummary?.TotalAmount ?? 0,
            TotalItems = totalItems
        };
    }

    public Task<List<OrderItem>> GetOrderItemsAsync(List<int> orderIds)
    {
        if (orderIds.Count == 0)
        {
            return Task.FromResult(new List<OrderItem>());
        }

        return _context.OrderItems
            .Where(x => orderIds.Contains(x.OrderId))
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<bool> UpdateOrderStatusAsync(int id, string status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order is null)
        {
            return false;
        }

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;

        return await _context.SaveChangesAsync() > 0;
    }
}
