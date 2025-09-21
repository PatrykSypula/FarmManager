using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseEmployeeWindow : Window
{
    public Employee? Employee { get; private set; }
    public ChooseEmployeeWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseEmployeeViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseEmployeeViewModel vm)
        {
            vm.RequestClose += employee =>
            {
                Employee = employee;
                DialogResult = true;
            };
        }
    }
}
