using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseDepositWindow : Window
{
    public Deposit? Deposit { get; private set; }
    public ChooseDepositWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseDepositViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseDepositViewModel vm)
        {
            vm.RequestClose += deposit =>
            {
                Deposit = deposit;
                DialogResult = true;
            };
        }
    }
}
