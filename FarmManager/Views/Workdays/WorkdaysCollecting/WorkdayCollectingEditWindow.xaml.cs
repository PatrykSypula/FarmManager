using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysCollecting;

public partial class WorkdayCollectingEditWindow : Window
{
    public WorkdayCollecting WorkdayCollecting { get; private set; }
    public WorkdayCollectingEditWindow(WorkdayCollecting workdayCollecting, ICollection<int> employeeIds, bool isEditable)
    {
        InitializeComponent();
        employeeIds.Remove(workdayCollecting.EmployeeId);
        Loaded += async (_, __) => await ((WorkdayCollectingEditViewModel)DataContext).InitializeAsync(workdayCollecting, employeeIds, isEditable);
        if (DataContext is WorkdayCollectingEditViewModel vm)
        {
            vm.RequestClose += workdayCollecting =>
            {
                WorkdayCollecting = workdayCollecting;
                DialogResult = true;
            };
        }
    }
}
