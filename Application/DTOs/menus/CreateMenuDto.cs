namespace Application.DTOs.menus;

public class CreateMenuDto
{
    public DateTime MenuDate { get; set; }
    public bool IsActive { get; set; } = true;
}
