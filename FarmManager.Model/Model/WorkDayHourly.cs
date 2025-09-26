using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayHourly : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public double Hours { get; set; }
    public double Price { get; set; }
    public double RemainingToPay { get; set; }
    public int WorkdayId { get; set; }
    public Workday Workday { get; set; }
}
