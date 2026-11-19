using System.Net;
using Application.DTOs.menus;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class MenuService(IMenuRepository menuRepository) : IMenuService
{
    private IMenuRepository repository = menuRepository;

    public async Task<ApiResponse<MenuDto>> GetActiveMenuAsync()
    {
        var menu = await repository.GetActiveMenuAsync();
        if (menu == null)
        {
            return new ApiResponse<MenuDto>(HttpStatusCode.NotFound, "Active menu not found.");
        }
        var items = menu.MenuItems.OrderBy(x => x.Id).ToList();
        return new ApiResponse<MenuDto>(HttpStatusCode.OK, "Active menu.", ToDto(menu, items));
    }

    public async Task<ApiResponse<MenuDto>> GetMenuByDateAsync(DateTime date)
    {
        var menu = await repository.GetMenuByDateAsync(date);
        if (menu == null)
        {
            return new ApiResponse<MenuDto>(HttpStatusCode.NotFound, "Menu not found.");
        }
        var items = menu.MenuItems.OrderBy(x => x.Id).ToList();
        return new ApiResponse<MenuDto>(HttpStatusCode.OK, "Menu with given date.", ToDto(menu, items));
    }

    public async Task<ApiResponse<string>> CreateMenuAsync(CreateMenuDto request)
    {
        var menu = new Menu()
        {
            MenuDate = DateTime.SpecifyKind(request.MenuDate.Date, DateTimeKind.Unspecified),
            IsActive = request.IsActive
        };
        var res = await repository.AddMenuAsync(menu);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.Created, "Added!")
        : new ApiResponse<string>(HttpStatusCode.InternalServerError, "Not added.");
    }

    public async Task<ApiResponse<string>> AddMenuItemAsync(int menuId, CreateMenuItemDto request)
    {
        var menu = await repository.GetMenuAsync(menuId);
        if (menu == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Menu not found.");
        }
        var menuItem = new MenuItem()
        {
            MenuId = menuId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category
        };
        var res = await repository.AddMenuItemAsync(menuItem);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.Created, "Added!")
        : new ApiResponse<string>(HttpStatusCode.InternalServerError, "Not added.");
    }

    public async Task<ApiResponse<List<string>>> GetMenuCategoriesAsync()
    {
        var res = await repository.GetMenuCategoriesAsync();
        return new ApiResponse<List<string>>(HttpStatusCode.OK, "List of categories.", res);
    }

    private MenuDto ToDto(Menu menu, List<MenuItem> items)
    {
        return new MenuDto()
        {
            Id = menu.Id,
            MenuDate = menu.MenuDate,
            IsActive = menu.IsActive,
            Items = items.Select(x => new MenuItemDto()
            {
                Id = x.Id,
                MenuId = x.MenuId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Category = x.Category
            }).ToList()
        };
    }
}