using System.Windows;
using FarmManager.App.ViewModels.EmployeeCosts;

namespace FarmManager.App.Views.EmployeeCosts;

public partial class EmployeeCostsWindow : Window
{
    public EmployeeCostsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((EmployeeCostsViewModel)DataContext).InitializeAsync();
    }
}
