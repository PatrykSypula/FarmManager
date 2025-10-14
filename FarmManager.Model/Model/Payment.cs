using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Payment : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal EmployeeCost { get; set; }
    public decimal PaymentQuantity { get; set; }
    public decimal RentCost { get; set; }
    public ICollection<PaymentWorkdayQuantity> WorkdayQuantity { get; set; } = [];
    public ICollection<int> EmployeeCosts { get; set; } = []; 
    public string? Description { get; set; }
}
