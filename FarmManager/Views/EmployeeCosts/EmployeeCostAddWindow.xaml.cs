using System.Windows;
using FarmManager.App.ViewModels.EmployeeCosts;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.EmployeeCosts;

public partial class EmployeeCostAddWindow : Window
{
    public EmployeeCost? EmployeeCost { get; private set; }
    public EmployeeCostAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((EmployeeCostAddViewModel)DataContext).InitializeAsync();
        if (DataContext is EmployeeCostAddViewModel vm)
        {
            vm.RequestClose += employeeCost =>
            {
                EmployeeCost = employeeCost;
                DialogResult = true;
            };
        }
    }
}
