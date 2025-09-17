using FarmManager.Model.Model;

namespace FarmManager.App.Models.Seasons;
public class SeasonAddModel
{
    public Season Season { get; set; } = new Season();
    public Plant Plant { get; set; } = new Plant();
}
