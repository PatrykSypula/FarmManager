using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Employees;
using FarmManager.App.Views.Employees;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Employees;

public class EmployeesViewModel(IEmployeeService employeeService) : BaseViewModel
{
    #region Properties

    public EmployeesModel Model = new EmployeesModel();

    public ObservableCollection<Employee> Employees
    {
        get { return Model.Employees; }
        set
        {
            Model.Employees = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Employees = new ObservableCollection<Employee>(await employeeService.GetAll(false));
    }

    public Employee SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenEmployeeAddWindow());
    private void OpenEmployeeAddWindow()
    {
        var window = new EmployeeAddWindow();
        if (window.ShowDialog() == true && window.Employee != null)
        {
            Employees.Add(window.Employee);
            OnPropertyChanged(nameof(Employees));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenEmployeeEditWindow());
    private void OpenEmployeeEditWindow()
    {
        var window = new EmployeeEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Employee != null)
        {
            var employee = window.Employee;
            var index = Employees.ToList().FindIndex(d => d.Id == employee.Id);

            if (index >= 0)
            {
                if (employee.IsDeleted)
                {
                    Employees.RemoveAt(index);
                }
                else
                {
                    Employees.RemoveAt(index);
                    Employees.Insert(index, employee);
                }
            }
            OnPropertyChanged(nameof(Employees));
        }
    }
}
