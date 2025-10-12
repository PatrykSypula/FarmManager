using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays.WorkdaysHourly;

public class WorkdayHourlyAddAllModel
{
    public ICollection<WorkdayHourly> WorkdaysHourly { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
    public decimal Price { get; set; }
    public decimal Hours { get; set; }
}
