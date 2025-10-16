using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;
public class SprayingBuyQuantity : BaseEntity
{
    public int SprayingId { get; set; }
    public Spraying Spraying { get; set; } = null!;
    public int BuyId { get; set; }
    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
    }

    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => _totalPrice;
        set => _totalPrice = Math.Round(value, 2);
    }

}

