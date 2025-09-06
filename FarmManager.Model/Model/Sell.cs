using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public Deposit Deposit { get; set; } = new Deposit();
    public WorkDay Harvest { get; set; } = new WorkDay();
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
