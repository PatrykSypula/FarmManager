using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Buy : BaseEntity, IDescribable
{
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public int FertilizerId { get; set; }
    public Fertilizer Fertilizer { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
}
