using FarmManager.Model.Model;

namespace FarmManager.App.Models.Sells;

public class SellAddModel
{
    public Sell Sell { get; set; } = new Sell();
    public Deposit Deposit { get; set; } = new Deposit();
    public Plant Plant { get; set; } = new Plant();
}
