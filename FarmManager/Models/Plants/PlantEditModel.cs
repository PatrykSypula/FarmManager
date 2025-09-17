using FarmManager.Model.Model;

namespace FarmManager.App.Models.Plants;

public class PlantEditModel
{
    public Plant Plant { get; set; } = new Plant();
    public Variety Variety { get; set; } = new Variety();
}
