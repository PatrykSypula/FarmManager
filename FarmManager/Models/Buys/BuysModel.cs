using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Buys;

public class BuysModel
{
    public ObservableCollection<Buy> Buys { get; set; }
    public Buy SelectedItem { get; set; }
}
