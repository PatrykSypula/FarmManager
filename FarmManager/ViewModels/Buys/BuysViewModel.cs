using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Buys;
using FarmManager.App.Models.Plants;
using FarmManager.App.Views.Buys;
using FarmManager.App.Views.Plants;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Buys;

public class BuysViewModel(IBuyService buyService) : BaseViewModel
{
    #region Properties

    public BuysModel Model = new BuysModel();

    public ObservableCollection<Buy> Buys
    {
        get { return Model.Buys; }
        set
        {
            Model.Buys = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Buys = new ObservableCollection<Buy>(await buyService.GetAll(false));
    }

    public Buy SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenBuyAddWindow());
    private void OpenBuyAddWindow()
    {
        var window = new BuyAddWindow();
        if (window.ShowDialog() == true && window.Buy != null)
        {
            Buys.Add(window.Buy);
            OnPropertyChanged(nameof(Buys));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenBuyEditWindow());
    private void OpenBuyEditWindow()
    {
        var window = new BuyEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Buy != null)
        {
            var buy = window.Buy;
            var index = Buys.ToList().FindIndex(d => d.Id == buy.Id);

            if (index >= 0)
            {
                if (buy.IsDeleted)
                {
                    Buys.RemoveAt(index);
                }
                else
                {
                    Buys.RemoveAt(index);
                    Buys.Insert(index, buy);
                }
            }
            OnPropertyChanged(nameof(Buys));
        }
    }
}
