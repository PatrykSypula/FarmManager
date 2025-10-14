using System.Windows;
using FarmManager.App.ViewModels.Employees;

namespace FarmManager.App.Views.Employees;

public partial class EmployeesWindow : Window
{
    public EmployeesWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((EmployeesViewModel)DataContext).InitializeAsync();
    }
}
