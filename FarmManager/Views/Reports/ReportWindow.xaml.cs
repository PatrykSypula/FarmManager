using System.Windows;
using FarmManager.App.ViewModels.Reports;

namespace FarmManager.App.Views.Reports;

public partial class ReportWindow : Window
{
    public ReportWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ReportViewModel)DataContext).InitializeAsync(id);
    }
}
