using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;
public class SprayingBuyQuantity : BaseEntity
{
    public int SprayingId { get; set; }
    public Spraying Spraying { get; set; } = null!;
    public int Buy { get; set; }
    public double Quantity { get; set; }
}

