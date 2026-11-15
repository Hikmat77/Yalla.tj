namespace Application.DTOs.orders;

public class CreateOrderDto
{
    public int CompanyId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = [];
}
