using System.Windows;
using FarmManager.App.ViewModels.Buys;
using FarmManager.App.ViewModels.Sells;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Buys;

public partial class BuyAddWindow : Window
{
    public Buy? Buy { get; private set; }
    public BuyAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((BuyAddViewModel)DataContext).InitializeAsync();
        if (DataContext is BuyAddViewModel vm)
        {
            vm.RequestClose += buy =>
            {
                Buy = buy;
                DialogResult = true;
            };
        }
    }
}
