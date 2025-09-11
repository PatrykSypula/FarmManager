using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Varieties;

public class VarietiesModel
{
    public ObservableCollection<Variety> Varieties { get; set; }
    public Variety SelectedItem { get; set; }
}
