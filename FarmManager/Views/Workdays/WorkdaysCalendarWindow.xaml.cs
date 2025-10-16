using System.Windows;
using FarmManager.App.ViewModels.Workdays;

namespace FarmManager.App.Views.Workdays;

public partial class WorkdaysCalendarWindow : Window
{
    public WorkdaysCalendarWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((WorkdaysCalendarViewModel)DataContext).InitializeAsync();
    }
}
