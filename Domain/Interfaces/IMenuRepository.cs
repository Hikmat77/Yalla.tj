using Domain.Models;

namespace Domain.Interfaces;

public interface IMenuRepository
{
    Task<bool> AddMenuAsync(Menu menu);
    Task<bool> AddMenuItemAsync(MenuItem menuItem);
    Task<Menu?> GetMenuAsync(int id);
    Task<Menu?> GetActiveMenuAsync();
    Task<Menu?> GetMenuByDateAsync(DateTime date);
    Task<List<MenuItem>> GetMenuItemsAsync(int menuId);
    Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> ids);
    Task<List<string>> GetMenuCategoriesAsync();
}
