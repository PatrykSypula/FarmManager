using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Fertilizers;

public class FertilizersModel
{
    public ObservableCollection<Fertilizer> Fertilizers { get; set; }
    public Fertilizer SelectedItem { get; set; }
}
