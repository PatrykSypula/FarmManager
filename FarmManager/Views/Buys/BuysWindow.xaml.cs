using System.Windows;
using FarmManager.App.ViewModels.Buys;

namespace FarmManager.App.Views.Buys;

public partial class BuysWindow : Window
{
    public BuysWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((BuysViewModel)DataContext).InitializeAsync();
    }
}
