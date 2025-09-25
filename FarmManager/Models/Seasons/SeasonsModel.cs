using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Seasons;

public class SeasonsModel
{
    public ObservableCollection<Season> Seasons { get; set; }
    public Season SelectedItem { get; set; }
}
