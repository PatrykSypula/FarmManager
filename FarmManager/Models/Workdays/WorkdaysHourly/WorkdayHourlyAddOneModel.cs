using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays.WorkdaysHourly;

public class WorkdayHourlyAddOneModel
{
    public WorkdayHourly WorkdayHourly { get; set; } = new WorkdayHourly();
    public Employee Employee { get; set; } = new Employee();
    public IEnumerable<int> EmployeeIds { get; set; }
}
