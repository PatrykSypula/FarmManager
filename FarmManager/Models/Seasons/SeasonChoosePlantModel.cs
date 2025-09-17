using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Seasons;
public class SeasonChoosePlantModel
{
    public ObservableCollection<Plant> Plants { get; set; }
    public Plant SelectedItem { get; set; }
}
