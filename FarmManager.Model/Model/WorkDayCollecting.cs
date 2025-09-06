using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkDayCollecting : BaseEntity
{
    public Employee Employee { get; set; } = new Employee();
    public int Quantity { get; set; }
    public double Price { get; set; }
}
