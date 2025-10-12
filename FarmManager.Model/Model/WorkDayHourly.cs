using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayHourly : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public decimal Hours { get; set; }
    public decimal Price { get; set; }
    public decimal RemainingToPay { get; set; }
    public int WorkdayId { get; set; }
    public Workday Workday { get; set; }
}
