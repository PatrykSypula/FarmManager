using System.Windows;
using FarmManager.App.ViewModels.Buys;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Buys;

public partial class BuyAddWindow : Window
{
    public Buy? Buy { get; private set; }
    public BuyAddWindow()
    {
        InitializeComponent();
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
