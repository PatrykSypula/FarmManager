using System.Windows;
using FarmManager.App.ViewModels.Sells;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Sells;

public partial class SellEditWindow : Window
{
    public Sell? Sell { get; private set; }
    public SellEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SellEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is SellEditViewModel vm)
        {
            vm.RequestClose += sell =>
            {
                Sell = sell;
                DialogResult = true;
            };
        }
    }
}
