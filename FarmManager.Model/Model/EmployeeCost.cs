using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class EmployeeCost : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    private decimal _quantity;
    public decimal Quantity { get => _quantity; set => _quantity = Math.Round(value, 2); }
    public bool IsPaid { get; set; } = false;
    public DateOnly Date { get; set; }
    public string? Description { get; set; }
}
