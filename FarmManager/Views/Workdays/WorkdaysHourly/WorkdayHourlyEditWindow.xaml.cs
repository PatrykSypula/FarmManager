using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHourlyEditWindow : Window
{
    public WorkdayHourly WorkdayHourly { get; private set; }
    public WorkdayHourlyEditWindow(WorkdayHourly workdayHourly, ICollection<int> employeeIds)
    {
        InitializeComponent();
        employeeIds.Remove(workdayHourly.EmployeeId);
        Loaded += async (_, __) => await ((WorkdayHourlyEditViewModel)DataContext).InitializeAsync(workdayHourly, employeeIds);
        if (DataContext is WorkdayHourlyEditViewModel vm)
        {
            vm.RequestClose += workdayHourly =>
            {
                WorkdayHourly = workdayHourly;
                DialogResult = true;
            };
        }

    }
}
