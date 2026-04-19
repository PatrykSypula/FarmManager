using FarmManager.Model.Model;

namespace FarmManager.App.Models.Investments;

public class InvestmentEditModel
{
    public Investment Investment { get; set; } = new Investment();
    public Plant Plant { get; set; } = new Plant();
}
