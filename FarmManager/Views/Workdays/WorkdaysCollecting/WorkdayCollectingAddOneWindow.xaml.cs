using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysCollecting;

public partial class WorkdayCollectingAddOneWindow : Window
{
    public WorkdayCollecting WorkdayCollecting { get; private set; }
    public WorkdayCollectingAddOneWindow(IEnumerable<int> employeeIds)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayCollectingAddOneViewModel)DataContext).InitializeAsync(employeeIds);
        if (DataContext is WorkdayCollectingAddOneViewModel vm)
        {
            vm.RequestClose += workdayCollecting =>
            {
                WorkdayCollecting = workdayCollecting;
                DialogResult = true;
            };
        }
    }
}
