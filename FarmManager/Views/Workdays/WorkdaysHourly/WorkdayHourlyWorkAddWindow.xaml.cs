using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHourlyWorkAddWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHourlyWorkAddWindow(DateOnly date, WorkdayType workdayType)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHourlyWorkAddViewModel)DataContext).InitializeAsync(date, workdayType);
        if (DataContext is WorkdayHourlyWorkAddViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
