using System.Windows;
using FarmManager.App.ViewModels.Deposits;

namespace FarmManager.App.Views.Deposits;

public partial class AddDepositWindow : Window
{
    public AddDepositWindow()
    {
        InitializeComponent();
        this.DataContext = new AddDepositViewModel();
    }
}
