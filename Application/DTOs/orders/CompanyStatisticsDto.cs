namespace Application.DTOs.orders;

public class CompanyStatisticsDto
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? LastOrderDate { get; set; }
}
