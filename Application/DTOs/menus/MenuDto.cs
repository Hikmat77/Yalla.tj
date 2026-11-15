namespace Application.DTOs.menus;

public class MenuDto
{
    public int Id { get; set; }
    public DateTime MenuDate { get; set; }
    public bool IsActive { get; set; }
    public List<MenuItemDto> Items { get; set; } = [];
}