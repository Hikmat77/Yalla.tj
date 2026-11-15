using System.Data.Common;
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddOrderAsync(Order order, List<OrderItem> items)
    {
        using var connection = (DbConnection)_context.Connection();
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();
        try
        {
            var orderQuery = "insert into orders (companyid, orderdate, status, totalamount) values (@companyid, @orderdate::date, @status, @totalamount) returning id";
            var orderId = await connection.ExecuteScalarAsync<int>(orderQuery, order, transaction);
            foreach (var item in items)
            {
                item.OrderId = orderId;
            }
            var itemsQuery = "insert into orderitems (orderid, menuitemid, quantity, price) values (@orderid, @menuitemid, @quantity, @price)";
            var res = await connection.ExecuteAsync(itemsQuery, items, transaction);
            await transaction.CommitAsync();
            return res>0;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<CompanyStatistics>> GetCompanyStatisticsAsync()
    {
        using var connection = _context.Connection();
        var query = @"select c.id as companyid, c.name as companyname, count(o.id)::int as totalorders, coalesce(sum(o.totalamount), 0) as totalamount, max(o.orderdate) as lastorderdate
                      from companies c
                      left join orders o on o.companyid = c.id
                      group by c.id, c.name
                      order by c.id";
        var statistics = await connection.QueryAsync<CompanyStatistics>(query);
        return statistics.ToList();
    }

    public async Task<List<Order>> GetCompanyOrdersAsync(int companyId)
    {
        using var connection = _context.Connection();
        var query = "select * from orders where companyid = @companyId order by orderdate desc, id desc";
        var orders = await connection.QueryAsync<Order>(query, new{companyId});
        return orders.ToList();
    }

    public async Task<DailySummary> GetDailySummaryAsync(DateTime date)
    {
        using var connection = _context.Connection();
        var query = @"select count(o.id)::int as totalorders, coalesce(sum(o.totalamount), 0) as totalamount,
                             coalesce((select sum(oi.quantity) from orderitems oi join orders o2 on o2.id = oi.orderid where o2.orderdate = @date::date), 0)::int as totalitems
                      from orders o
                      where o.orderdate = @date::date";
        var summary = await connection.QuerySingleAsync<DailySummary>(query, new{date});
        return summary;
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync(List<int> orderIds)
    {
        using var connection = _context.Connection();
        var query = "select * from orderitems where orderid = any(@orderIds) order by id";
        var items = await connection.QueryAsync<OrderItem>(query, new{orderIds = orderIds.ToArray()});
        return items.ToList();
    }

    public async Task<bool> UpdateOrderStatusAsync(int id, string status)
    {
        using var connection = _context.Connection();
        var query = "update orders set status = @status, updatedat = current_timestamp where id = @id";
        var res = await connection.ExecuteAsync(query, new{id, status});
        return res>0;
    }
}