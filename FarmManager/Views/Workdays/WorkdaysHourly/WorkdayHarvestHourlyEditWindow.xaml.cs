using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHarvestHourlyEditWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHarvestHourlyEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHarvestHourlyEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is WorkdayHarvestHourlyEditViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
