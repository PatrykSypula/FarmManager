using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Buy : DetailedEntity
{
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Vendor Vendor { get; set; } = null!;
}
