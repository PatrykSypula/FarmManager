using System.Windows;
using FarmManager.App.ViewModels.Sells;

namespace FarmManager.App.Views.Sells;

public partial class SellsWindow : Window
{
    public SellsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SellsViewModel)DataContext).InitializeAsync();
    }
}
