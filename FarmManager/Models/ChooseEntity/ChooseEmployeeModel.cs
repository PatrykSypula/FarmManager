using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;
public class ChooseEmployeeModel
{
    public ObservableCollection<Employee> Employees { get; set; }
    public Employee SelectedItem { get; set; }
}
