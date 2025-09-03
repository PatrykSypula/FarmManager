using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public Deposit Deposit { get; set; } = null!;
    public WorkDay Harvest { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
