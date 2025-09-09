using System.Windows;
using FarmManager.App.ViewModels.Employees;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Employees;

public partial class EmployeeAddWindow : Window
{
    public Employee? Employee { get; private set; }
    public EmployeeAddWindow()
    {
        InitializeComponent();
        if (DataContext is EmployeeAddViewModel vm)
        {
            vm.RequestClose += employee =>
            {
                Employee = employee;
                DialogResult = true;
            };
        }
    }
}
