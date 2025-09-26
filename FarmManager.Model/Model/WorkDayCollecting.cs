using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayCollecting : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double RemainingToPay { get; set; }
}
