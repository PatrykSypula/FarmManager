using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Sells;

public class SellsModel
{
    public ObservableCollection<Sell> Sells { get; set; }
    public Sell SelectedItem { get; set; }
}
