using FarmManager.Model.Model;

namespace FarmManager.App.Models.Investments;

public class InvestmentAddModel
{
    public Investment Investment { get; set; } = new Investment();
    public Plant Plant { get; set; } = new Plant();
}
