using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class MenuRepository(AppDbContext appDbContext) : IMenuRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddMenuAsync(Menu menu)
    {
        using var connection = _context.Connection();
        var query = "insert into menus (menudate, isactive) values (@menudate::date, @isactive)";
        var res = await connection.ExecuteAsync(query, menu);
        return res>0;
    }

    public async Task<bool> AddMenuItemAsync(MenuItem menuItem)
    {
        using var connection = _context.Connection();
        var query = "insert into menuitems (menuid, name, description, price, category) values (@menuid, @name, @description, @price, @category)";
        var res = await connection.ExecuteAsync(query, menuItem);
        return res>0;
    }

    public async Task<Menu?> GetMenuAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "select * from menus where id = @id";
        var menu = await connection.QueryFirstOrDefaultAsync<Menu>(query, new{id});
        return menu;
    }

    public async Task<Menu?> GetActiveMenuAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from menus where isactive = true order by menudate desc, id desc limit 1";
        var menu = await connection.QueryFirstOrDefaultAsync<Menu>(query);
        return menu;
    }

    public async Task<Menu?> GetMenuByDateAsync(DateTime date)
    {
        using var connection = _context.Connection();
        var query = "select * from menus where menudate = @date::date order by isactive desc, id desc limit 1";
        var menu = await connection.QueryFirstOrDefaultAsync<Menu>(query, new{date});
        return menu;
    }

    public async Task<List<MenuItem>> GetMenuItemsAsync(int menuId)
    {
        using var connection = _context.Connection();
        var query = "select * from menuitems where menuid = @menuId order by id";
        var menuItems = await connection.QueryAsync<MenuItem>(query, new{menuId});
        return menuItems.ToList();
    }

    public async Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> ids)
    {
        using var connection = _context.Connection();
        var query = "select * from menuitems where id = any(@ids)";
        var menuItems = await connection.QueryAsync<MenuItem>(query, new{ids = ids.ToArray()});
        return menuItems.ToList();
    }

    public async Task<List<string>> GetMenuCategoriesAsync()
    {
        using var connection = _context.Connection();
        var query = "select distinct category from menuitems order by category";
        var categories = await connection.QueryAsync<string>(query);
        return categories.ToList();
    }
}