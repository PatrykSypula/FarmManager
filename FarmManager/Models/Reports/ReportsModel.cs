using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Reports;

public class ReportsModel
{
    public ObservableCollection<Season> Seasons { get; set; }
    public Season SelectedItem { get; set; }
}
