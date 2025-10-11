using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class PaymentWorkdayQuantity : BaseEntity
{
    public int PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;
    public int WorkdayId { get; set; }
    public double Quantity { get; set; }
}
