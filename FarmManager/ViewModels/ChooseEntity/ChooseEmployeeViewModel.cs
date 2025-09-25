using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseEmployeeViewModel(IEmployeeService employeeService) : BaseViewModel
{
    #region Properties

    public event Action<Employee>? RequestClose;

    public ChooseEmployeeModel Model = new ChooseEmployeeModel();

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
        Employees = new ObservableCollection<Employee>(await employeeService.GetAll());
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

    public RelayCommand Select => new RelayCommand(execute => SelectEmployee());
    private void SelectEmployee()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
