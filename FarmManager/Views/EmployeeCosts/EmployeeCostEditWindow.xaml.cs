using System.Windows;
using FarmManager.App.ViewModels.EmployeeCosts;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.EmployeeCosts;

public partial class EmployeeCostEditWindow : Window
{
    public EmployeeCost? EmployeeCost { get; private set; }
    public EmployeeCostEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((EmployeeCostEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is EmployeeCostEditViewModel vm)
        {
            vm.RequestClose += employeeCost =>
            {
                EmployeeCost = employeeCost;
                DialogResult = true;
            };
        }
    }
}
