namespace Application.DTOs.orders;

public class DailySummaryDto
{
    public DateTime Date { get; set; }
    public int TotalOrders { get; set; }
    public int TotalItems { get; set; }
    public decimal TotalAmount { get; set; }
}