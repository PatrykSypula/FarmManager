using System.Windows;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Deposits;

public partial class DepositEditWindow : Window
{
    public Deposit? Deposit { get; private set; }
    public DepositEditWindow(Guid id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((DepositEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is DepositEditViewModel vm)
        {
            vm.RequestClose += deposit =>
            {
                Deposit = deposit;
                DialogResult = true;
            };
        }
    }
}
