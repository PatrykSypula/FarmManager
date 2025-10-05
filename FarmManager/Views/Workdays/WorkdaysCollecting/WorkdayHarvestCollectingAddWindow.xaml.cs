using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysCollecting;

public partial class WorkdayHarvestCollectingAddWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHarvestCollectingAddWindow(DateOnly date, WorkdayType workdayType)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHarvestCollectingAddViewModel)DataContext).InitializeAsync(date, workdayType);
        if (DataContext is WorkdayHarvestCollectingAddViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
