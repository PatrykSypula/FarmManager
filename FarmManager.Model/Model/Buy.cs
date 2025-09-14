using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Buy : BaseEntity, IDescribable
{
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public Fertilizer Fertilizer { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
