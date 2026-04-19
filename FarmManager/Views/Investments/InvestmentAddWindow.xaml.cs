using System.Windows;
using FarmManager.App.ViewModels.Investments;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Investments;

public partial class InvestmentAddWindow : Window
{
    public Investment? Investment { get; private set; }
    public InvestmentAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((InvestmentAddViewModel)DataContext).InitializeAsync();
        if (DataContext is InvestmentAddViewModel vm)
        {
            vm.RequestClose += investment =>
            {
                Investment = investment;
                DialogResult = true;
            };
        }
    }
}
