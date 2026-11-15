using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class SubscriptionRepository(AppDbContext appDbContext) : ISubscriptionRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddSubscriptionAsync(Subscription subscription)
    {
        using var connection = _context.Connection();
        var query = "insert into subscriptions (companyid, plantype, mealsperday, price, startdate, enddate, isactive) values (@companyid, @plantype, @mealsperday, @price, @startdate::date, @enddate::date, @isactive)";
        var res = await connection.ExecuteAsync(query, subscription);
        return res>0;
    }

    public async Task<List<Subscription>> GetActiveSubscriptionsAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from subscriptions where isactive = true order by id";
        var subscriptions = await connection.QueryAsync<Subscription>(query);
        return subscriptions.ToList();
    }

    public async Task<List<Subscription>> GetCompanySubscriptionsAsync(int companyId)
    {
        using var connection = _context.Connection();
        var query = "select * from subscriptions where companyid = @companyId order by id";
        var subscriptions = await connection.QueryAsync<Subscription>(query, new{companyId});
        return subscriptions.ToList();
    }

    public async Task<bool> UpdateSubscriptionStatusAsync(int id, bool isActive)
    {
        using var connection = _context.Connection();
        var query = "update subscriptions set isactive = @isActive, updatedat = current_timestamp where id = @id";
        var res = await connection.ExecuteAsync(query, new{id, isActive});
        return res>0;
    }
}