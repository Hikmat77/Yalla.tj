namespace Domain.Models;

public class Menu
{
    public int Id { get; set; }
    public DateTime MenuDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<MenuItem> MenuItems { get; set; } = [];
}
