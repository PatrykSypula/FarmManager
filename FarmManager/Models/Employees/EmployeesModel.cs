using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Employees;

public class EmployeesModel
{
    public ObservableCollection<Employee> Employees { get; set; }
    public Employee SelectedItem { get; set; }
}
