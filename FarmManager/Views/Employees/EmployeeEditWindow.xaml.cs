using System.Windows;
using FarmManager.App.ViewModels.Employees;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Employees;

public partial class EmployeeEditWindow : Window
{
    public Employee? Employee { get; private set; }
    public EmployeeEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((EmployeeEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is EmployeeEditViewModel vm)
        {
            vm.RequestClose += employee =>
            {
                Employee = employee;
                DialogResult = true;
            };
        }
    }
}
