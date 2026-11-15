namespace Application.DTOs.subscriptions;

public class CreateSubscriptionDto
{
    public int CompanyId { get; set; }
    public string PlanType { get; set; } = string.Empty;
    public int MealsPerDay { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}