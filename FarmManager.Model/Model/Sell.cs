using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public int DepositId { get; set; }
    public Deposit Deposit { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ICollection<SellHarvestQuantity> HarvestQuantity { get; set; } = [];
}
