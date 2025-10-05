using System.Windows;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays.WorkdaysHourly;

public partial class WorkdayHourlyWorkEditWindow : Window
{
    public Workday? Workday { get; private set; }
    public WorkdayHourlyWorkEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdayHourlyWorkEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is WorkdayHourlyWorkEditViewModel vm)
        {
            vm.RequestClose += workday =>
            {
                Workday = workday;
                DialogResult = true;
            };
        }
    }
}
