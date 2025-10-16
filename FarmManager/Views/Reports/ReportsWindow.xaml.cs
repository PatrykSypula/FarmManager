using System.Windows;
using FarmManager.App.ViewModels.Reports;

namespace FarmManager.App.Views.Reports;

public partial class ReportsWindow : Window
{
    public ReportsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ReportsViewModel)DataContext).InitializeAsync();
    }
}
