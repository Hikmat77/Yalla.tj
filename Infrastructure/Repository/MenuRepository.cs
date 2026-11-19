using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class MenuRepository(AppDbContext appDbContext) : IMenuRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddMenuAsync(Menu menu)
    {
        await _context.Menus.AddAsync(menu);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddMenuItemAsync(MenuItem menuItem)
    {
        await _context.MenuItems.AddAsync(menuItem);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<Menu?> GetMenuAsync(int id)
    {
        return _context.Menus.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Menu?> GetActiveMenuAsync()
    {
        return _context.Menus
            .Where(x => x.IsActive)
            .Include(x => x.MenuItems)
            .OrderByDescending(x => x.MenuDate)
            .ThenByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public Task<Menu?> GetMenuByDateAsync(DateTime date)
    {
        return _context.Menus
            .Where(x => x.MenuDate == date.Date)
            .Include(x => x.MenuItems)
            .OrderByDescending(x => x.IsActive)
            .ThenByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public Task<List<MenuItem>> GetMenuItemsAsync(int menuId)
    {
        return _context.MenuItems
            .Where(x => x.MenuId == menuId)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> ids)
    {
        if (ids.Count == 0)
        {
            return Task.FromResult(new List<MenuItem>());
        }

        return _context.MenuItems.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public Task<List<string>> GetMenuCategoriesAsync()
    {
        return _context.MenuItems
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }
}
