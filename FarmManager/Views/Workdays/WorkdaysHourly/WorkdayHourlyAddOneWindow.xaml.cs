using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHourlyAddOneWindow : Window
{
    public WorkdayHourly WorkdayHourly { get; private set; }
    public WorkdayHourlyAddOneWindow(IEnumerable<int> employeeIds)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHourlyAddOneViewModel)DataContext).InitializeAsync(employeeIds);
        if (DataContext is WorkdayHourlyAddOneViewModel vm)
        {
            vm.RequestClose += workdayHourly =>
            {
                WorkdayHourly = workdayHourly;
                DialogResult = true;
            };
        }
    }
}
