using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class EmployeeCost : BaseEntity
{
    public Employee Employee { get; set; } = new Employee();
    public double Quantity { get; set; }
}
