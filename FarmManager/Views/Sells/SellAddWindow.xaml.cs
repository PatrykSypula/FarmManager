using System.Windows;
using FarmManager.App.ViewModels.Sells;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Sells;

public partial class SellAddWindow : Window
{
    public Sell? Sell { get; private set; }
    public SellAddWindow()
    {
        InitializeComponent();
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
