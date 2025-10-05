using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays.WorkdaysCollecting;

public class WorkdayCollectingAddAllModel
{
    public ICollection<WorkdayCollecting> WorkdaysCollecting { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
    public double Price { get; set; }
}
