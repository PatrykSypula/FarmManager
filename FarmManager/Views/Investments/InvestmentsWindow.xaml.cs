using System.Windows;
using FarmManager.App.ViewModels.Investments;

namespace FarmManager.App.Views.Investments;

public partial class InvestmentsWindow : Window
{
    public InvestmentsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((InvestmentsViewModel)DataContext).InitializeAsync();
    }
}
