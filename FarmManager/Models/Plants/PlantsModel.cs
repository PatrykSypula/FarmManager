using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Plants;

public class PlantsModel
{
    public ObservableCollection<Plant> Plants { get; set; }
    public Plant SelectedItem { get; set; }
}
