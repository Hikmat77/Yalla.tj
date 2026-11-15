using Application.DTOs.menus;
using Application.Responses;

namespace Application.Interfaces;

public interface IMenuService
{
    Task<ApiResponse<MenuDto>> GetActiveMenuAsync();
    Task<ApiResponse<MenuDto>> GetMenuByDateAsync(DateTime date);
    Task<ApiResponse<string>> CreateMenuAsync(CreateMenuDto request);
    Task<ApiResponse<string>> AddMenuItemAsync(int menuId, CreateMenuItemDto request);
    Task<ApiResponse<List<string>>> GetMenuCategoriesAsync();
}