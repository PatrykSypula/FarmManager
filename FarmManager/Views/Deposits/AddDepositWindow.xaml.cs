using System.Windows;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.Views.Deposits;

public partial class AddDepositWindow : Window
{
    public AddDepositWindow()
    {
        InitializeComponent();
        if (DataContext is AddDepositViewModel vm)
        {
            vm.RequestClose += () => this.Close();
        }
    }
}
