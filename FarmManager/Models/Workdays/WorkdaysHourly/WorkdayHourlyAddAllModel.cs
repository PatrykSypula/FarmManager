using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays.WorkdaysHourly;

public class WorkdayHourlyAddAllModel
{
    public ICollection<WorkdayHourly> WorkdaysHourly { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
    public double Price { get; set; }
    public double Hours { get; set; }
}
