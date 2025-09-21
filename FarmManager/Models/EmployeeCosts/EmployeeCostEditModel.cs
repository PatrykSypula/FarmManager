using FarmManager.Model.Model;

namespace FarmManager.App.Models.EmployeeCosts;

public class EmployeeCostEditModel
{
    public EmployeeCost EmployeeCost { get; set; } = new EmployeeCost();
    public Employee Employee { get; set; } = new Employee();
}
