using System.Windows;
using FarmManager.App.ViewModels.Workdays;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays;

public partial class WorkdayAddWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayAddWindow(DateOnly date)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayAddViewModel)DataContext).InitializeAsync(date);
        if (DataContext is WorkdayAddViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
