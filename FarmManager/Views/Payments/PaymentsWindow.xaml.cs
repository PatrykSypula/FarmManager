using System.Windows;
using FarmManager.App.ViewModels.Payments;

namespace FarmManager.App.Views.Payments;

public partial class PaymentsWindow : Window
{
    public PaymentsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((PaymentsViewModel)DataContext).InitializeAsync();
    }
}
