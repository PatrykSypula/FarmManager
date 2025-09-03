using FarmManager.App.Helpers;
using FarmManager.App.Models;
using FarmManager.App.Views.Deposits;

namespace FarmManager.App.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MainWindowModel mainWindowModel = new MainWindowModel();
    public string Title
    {
        get
        {
            return mainWindowModel.Title;
        }
        set
        {
            mainWindowModel.Title = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand AddDeposit => new RelayCommand(execute => OpenAddDepositWindow());

    private void OpenAddDepositWindow()
    {
        new AddDepositWindow().ShowDialog();
    }
}
