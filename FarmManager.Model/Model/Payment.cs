using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Payment : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public decimal Quantity { get; set; }
    public ICollection<PaymentWorkdayQuantity> WorkdayQuantity { get; set; } = [];
}
