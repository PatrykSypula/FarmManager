using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Sprayings;

public class SprayingsModel
{
    public ObservableCollection<Spraying> Sprayings { get; set; }
    public Spraying SelectedItem { get; set; }
}
