using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class EmployeeCost : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public double Quantity { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Description { get; set; }
}
