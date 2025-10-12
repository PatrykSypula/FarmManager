using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class SellHarvestQuantity : BaseEntity
{
    public int SellId { get; set; }
    public Sell Sell { get; set; } = null!;
    public int HarvestId { get; set; }
    public decimal Quantity { get; set; }
}
