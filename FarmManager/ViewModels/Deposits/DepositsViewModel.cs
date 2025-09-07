using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Views.Deposits;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Deposits;

public class DepositsViewModel(IDepositService depositService) : BaseViewModel
{
    public DepositsModel Model = new DepositsModel();

    public ObservableCollection<Deposit> Deposits
    {
        get { return Model.Deposits; }
        set
        {
            Model.Deposits = value;
            OnPropertyChanged();
        }
    }

    public Deposit SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Deposits  = new ObservableCollection<Deposit>(await depositService.GetAll());
    }


    public RelayCommand CreateDeposit => new RelayCommand(execute => OpenDepositAddWindow());
    private void OpenDepositAddWindow()
    {
        var window = new DepositAddWindow();
        if (window.ShowDialog() == true && window.Deposit != null)
        {
            Deposits.Add(window.Deposit);
            OnPropertyChanged(nameof(Deposits));
        }
    }

    public RelayCommand EditDeposit => new RelayCommand(execute => OpenDepositEditWindow());
    private void OpenDepositEditWindow()
    {
        var window = new DepositEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Deposit != null)
        {
            var deposit = window.Deposit;
            var index = Deposits.ToList().FindIndex(d => d.Id == deposit.Id);

            if (index >= 0)
            {
                if (deposit.IsDeleted)
                {
                    Deposits.RemoveAt(index);
                }
                else
                {
                    Deposits.RemoveAt(index);
                    Deposits.Insert(index, deposit);
                }
            }
            OnPropertyChanged(nameof(Deposits));
        }
    }
}
