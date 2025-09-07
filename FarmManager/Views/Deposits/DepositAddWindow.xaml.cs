using System.Windows;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.Views.Deposits;

public partial class DepositAddWindow : Window
{
    public Deposit? Deposit { get; private set; }
    public DepositAddWindow()
    {
        InitializeComponent();
        if (DataContext is DepositAddViewModel vm)
        {
            vm.RequestClose += deposit =>
            {
                Deposit = deposit;
                DialogResult = true;
            };
        }
    }
}
