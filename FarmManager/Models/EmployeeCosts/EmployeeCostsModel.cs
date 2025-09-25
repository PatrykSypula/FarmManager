using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.EmployeeCosts;

public class EmployeeCostsModel
{
    public ObservableCollection<EmployeeCost> EmployeeCosts { get; set; }
    public EmployeeCost SelectedItem { get; set; }
}
