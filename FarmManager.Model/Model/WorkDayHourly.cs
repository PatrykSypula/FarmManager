using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkDayHourly : BaseEntity
{
    public Employee Employee { get; set; } = new Employee();
    public double Hours { get; set; }
}
