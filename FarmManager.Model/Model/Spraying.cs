using System.Net;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Spraying : BaseEntity
{
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
    public int FertilizerId { get; set; }
    public Fertilizer Fertilizer { get; set; } = null!;
    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
    }

    public DateOnly Date { get; set; }
    public string? Description { get; set; }
    public ICollection<SprayingBuyQuantity> BuyQuantity { get; set; } = [];
}
