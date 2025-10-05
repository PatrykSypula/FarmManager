using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays.WorkdaysCollecting;

public class WorkdayCollectingEditModel
{
    public WorkdayCollecting WorkdayCollecting { get; set; } = new WorkdayCollecting();
    public Employee Employee { get; set; } = new Employee();
    public IEnumerable<int> EmployeeIds { get; set; }
}
