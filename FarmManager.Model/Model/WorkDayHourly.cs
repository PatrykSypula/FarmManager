using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkDayHourly : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public double Hours { get; set; }
}
