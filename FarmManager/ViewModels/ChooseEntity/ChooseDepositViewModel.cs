using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseDepositViewModel(IDepositService depositService) : BaseViewModel
{
    #region Properties

    public event Action<Deposit>? RequestClose;

    public ChooseDepositModel Model = new ChooseDepositModel();

    public ObservableCollection<Deposit> Deposits
    {
        get { return Model.Deposits; }
        set
        {
            Model.Deposits = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Deposits = new ObservableCollection<Deposit>(await depositService.GetAll());
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

    #endregion

    public RelayCommand Select => new RelayCommand(execute => SelectDeposit());
    private void SelectDeposit()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
