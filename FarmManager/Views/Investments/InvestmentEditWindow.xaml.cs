using System.Windows;
using FarmManager.App.ViewModels.Investments;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Investments;

public partial class InvestmentEditWindow : Window
{
    public Investment? Investment { get; private set; }
    public InvestmentEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((InvestmentEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is InvestmentEditViewModel vm)
        {
            vm.RequestClose += investment =>
            {
                Investment = investment;
                DialogResult = true;
            };
        }
    }
}
