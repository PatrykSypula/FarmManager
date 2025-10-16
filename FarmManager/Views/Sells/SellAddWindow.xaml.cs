using System.Windows;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.App.ViewModels.Sells;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Sells;

public partial class SellAddWindow : Window
{
    public Sell? Sell { get; private set; }
    public SellAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SellAddViewModel)DataContext).InitializeAsync();
        if (DataContext is SellAddViewModel vm)
        {
            vm.RequestClose += sell =>
            {
                Sell = sell;
                DialogResult = true;
            };
        }
    }
}
