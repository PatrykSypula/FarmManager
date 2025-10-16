using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHourlyAddAllWindow : Window
{
    public ICollection<WorkdayHourly> WorkdaysHourly { get; private set; }
    public WorkdayHourlyAddAllWindow(IEnumerable<int> employeeIds)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHourlyAddAllViewModel)DataContext).InitializeAsync(employeeIds);
        if (DataContext is WorkdayHourlyAddAllViewModel vm)
        {
            vm.RequestClose += workdaysHourly =>
            {
                WorkdaysHourly = workdaysHourly;
                DialogResult = true;
            };
        }
    }
}
