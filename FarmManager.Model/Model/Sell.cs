using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public int DepositId { get; set; }
    public Deposit Deposit { get; set; } = null!;
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Description { get; set; }
    public ICollection<SellHarvestQuantity> HarvestQuantity { get; set; } = [];
}
