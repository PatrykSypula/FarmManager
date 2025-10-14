using System.Net;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Spraying : BaseEntity
{
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
    public int FertilizerId { get; set; }
    public Fertilizer Fertilizer { get; set; } = null!;
    public decimal Quantity { get; set; }
    public DateOnly Date { get; set; }
    public string? Description { get; set; }
    public ICollection<SprayingBuyQuantity> BuyQuantity { get; set; } = [];
}
