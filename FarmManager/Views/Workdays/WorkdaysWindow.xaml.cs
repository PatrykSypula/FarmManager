using System.Windows;
using FarmManager.App.ViewModels.Workdays;

namespace FarmManager.App.Views.Workdays;

public partial class WorkdaysWindow : Window
{
    public WorkdaysWindow()
    {
        InitializeComponent();
        //Loaded += async (_, __) => await ((WorkdaysViewModel)DataContext).InitializeAsync();
    }
}
