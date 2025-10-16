using System.Collections.ObjectModel;
using System.Security.Permissions;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays;

public class WorkdaysModel
{
    public ObservableCollection<Workday> Workdays { get; set; }
    public Workday SelectedItem { get; set; }
    public DateOnly Date { get; set; }
}
