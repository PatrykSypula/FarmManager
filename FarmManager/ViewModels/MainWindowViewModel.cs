using FarmManager.App.Helpers;
using FarmManager.App.Models;
using FarmManager.App.Views.Deposits;

namespace FarmManager.App.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private MainWindowModel model = new MainWindowModel();
    public string Title
    {
        get
        {
            return model.Title;
        }
        set
        {
            model.Title = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand AddDeposit => new RelayCommand(execute => OpenAddDepositWindow());

    private void OpenAddDepositWindow()
    {
        new DepositsWindow().ShowDialog();
    }
}
