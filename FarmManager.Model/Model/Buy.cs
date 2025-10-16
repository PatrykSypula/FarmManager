using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Buy : BaseEntity, IDescribable
{
    public decimal Price { get; set; }
    private decimal _quantity;
    public decimal Quantity { get => _quantity; set => _quantity = Math.Round(value, 2); }
    private decimal _remainingQuantity;
    public decimal RemainingQuantity { get => _remainingQuantity; set => _remainingQuantity = Math.Round(value, 2); }
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public int FertilizerId { get; set; }
    public Fertilizer Fertilizer { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
}
