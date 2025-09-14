using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Plants;

public class PlantChooseVarietyModel
{
    public ObservableCollection<Variety> Varieties { get; set; }
    public Variety SelectedItem { get; set; }
}
