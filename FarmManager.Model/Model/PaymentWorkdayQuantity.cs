using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class PaymentWorkdayQuantity : BaseEntity
{
    public int PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;
    public int? WorkdayCollectingId { get; set; }
    public int? WorkdayHourlyId { get; set; }
    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
    }

}
