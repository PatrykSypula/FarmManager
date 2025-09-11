using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Sell : BaseEntity
{
    public Deposit Deposit { get; set; } = new Deposit();
    public Harvest Harvest { get; set; } = new Harvest();
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
