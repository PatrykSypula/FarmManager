using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysCollecting;

public partial class WorkdayHarvestCollectingEditWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHarvestCollectingEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHarvestCollectingEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is WorkdayHarvestCollectingEditViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
