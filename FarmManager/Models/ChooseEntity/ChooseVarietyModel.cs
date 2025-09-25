using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;

public class ChooseVarietyModel
{
    public ObservableCollection<Variety> Varieties { get; set; }
    public Variety SelectedItem { get; set; }
}
