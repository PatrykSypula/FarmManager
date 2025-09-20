using FarmManager.Model.Model;

namespace FarmManager.App.Models.Sprayings;

public class SprayingEditModel
{
    public Spraying Spraying { get; set; } = new Spraying();
    public Plant Plant { get; set; } = new Plant();
    public Fertilizer Fertilizer { get; set; } = new Fertilizer();
}
