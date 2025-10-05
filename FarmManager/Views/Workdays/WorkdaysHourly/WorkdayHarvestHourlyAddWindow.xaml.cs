using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHarvestHourlyAddWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHarvestHourlyAddWindow(DateOnly date, WorkdayType workdayType)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHarvestHourlyAddViewModel)DataContext).InitializeAsync(date, workdayType);
        if (DataContext is WorkdayHarvestHourlyAddViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
