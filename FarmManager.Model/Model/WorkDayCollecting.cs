using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayCollecting : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal RemainingToPay { get; set; }
    public int WorkdayId { get; set; }
    public Workday Workday { get; set; }
}
