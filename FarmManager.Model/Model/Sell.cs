using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public int DepositId { get; set; }
    public Deposit Deposit { get; set; } = null!;
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
    private decimal _price;
    public decimal Price
    {
        get => _price;
        set => _price = Math.Round(value, 2);
    }

    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
    }

    public DateOnly Date { get; set; }
    public string? Description { get; set; }
    public ICollection<SellHarvestQuantity> HarvestQuantity { get; set; } = [];
}
