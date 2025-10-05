using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysCollecting;

public partial class WorkdayCollectingAddAllWindow : Window
{
    public ICollection<WorkdayCollecting> WorkdaysCollecting { get; private set; }
    public WorkdayCollectingAddAllWindow(IEnumerable<int> employeeIds)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayCollectingAddAllViewModel)DataContext).InitializeAsync(employeeIds);
        if (DataContext is WorkdayCollectingAddAllViewModel vm)
        {
            vm.RequestClose += workdaysCollecting =>
            {
                WorkdaysCollecting = workdaysCollecting;
                DialogResult = true;
            };
        }
    }
}
