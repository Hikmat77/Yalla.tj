using Application.DTOs.menus;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenusController(
    IMenuService menuService,
    ILogger<MenusController> logger) : ControllerBase
{
    private IMenuService service = menuService;
    private ILogger<MenusController> _logger = logger;

    [HttpGet("active")]
    public async Task<ApiResponse<MenuDto>> GetActiveMenu()
    {
        _logger.LogInformation("GET api/Menus/active {date}", DateTime.Now);
        var res = await service.GetActiveMenuAsync();
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("{date:datetime}")]
    public async Task<ApiResponse<MenuDto>> GetMenuByDate(DateTime date)
    {
        _logger.LogInformation("GET api/Menus/{date} {date}", date, DateTime.Now);
        var res = await service.GetMenuByDateAsync(date);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<string>> AddMenu(CreateMenuDto request)
    {
        _logger.LogInformation("POST api/Menus {date}", DateTime.Now);
        var res = await service.CreateMenuAsync(request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPost("{menuId:int}/items")]
    public async Task<ApiResponse<string>> AddMenuItem(int menuId, CreateMenuItemDto request)
    {
        _logger.LogInformation("POST api/Menus/{menuId}/items {date}", menuId, DateTime.Now);
        var res = await service.AddMenuItemAsync(menuId, request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("categories")]
    public async Task<ApiResponse<List<string>>> GetMenuCategories()
    {
        _logger.LogInformation("GET api/Menus/categories {date}", DateTime.Now);
        var res = await service.GetMenuCategoriesAsync();
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }
}