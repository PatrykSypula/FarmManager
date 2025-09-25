using FarmManager.Model.Model;

namespace FarmManager.App.Models.Buys;

public class BuyAddModel
{
    public Buy Buy { get; set; } = new Buy();
    public Fertilizer Fertilizer { get; set; } = new Fertilizer();
    public Vendor Vendor { get; set; } = new Vendor();
}
