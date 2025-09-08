using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Diseases;

public class DiseasesModel
{
    public ObservableCollection<Disease> Diseases { get; set; }
    public Disease SelectedItem { get; set; }
}
