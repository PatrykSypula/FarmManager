using System.Windows;
using FarmManager.App.ViewModels.Buys;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Buys;

public partial class BuyEditWindow : Window
{
    public Buy? Buy { get; private set; }
    public BuyEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((BuyEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is BuyEditViewModel vm)
        {
            vm.RequestClose += buy =>
            {
                Buy = buy;
                DialogResult = true;
            };
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
