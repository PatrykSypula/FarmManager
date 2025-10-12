using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Sells;
using FarmManager.App.Views.Sells;
using FarmManager.App.Views.Sprayings;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Sells;

public class SellsViewModel(ISellService sellService) : BaseViewModel
{
    #region Properties

    public SellsModel Model = new SellsModel();

    public ObservableCollection<Sell> Sells
    {
        get { return Model.Sells; }
        set
        {
            Model.Sells = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Sells = new ObservableCollection<Sell>(await sellService.GetAll(false));
    }

    public Sell SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenSellAddWindow());
    private async void OpenSellAddWindow()
    {
        var window = new SellAddWindow();
        if (window.ShowDialog() == true && window.Sell != null)
        {
            var spraying = await sellService.Get(window.Sell.Id);
            Sells.Add(spraying);
            OnPropertyChanged(nameof(Sprayings));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenSellEditWindow());
    private void OpenSellEditWindow()
    {
        var window = new SellEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Sell != null)
        {
            var sell = window.Sell;
            var index = Sells.ToList().FindIndex(d => d.Id == sell.Id);

            if (index >= 0)
            {
                if (sell.IsDeleted)
                {
                    Sells.RemoveAt(index);
                }
                else
                {
                    Sells.RemoveAt(index);
                    Sells.Insert(index, sell);
                }
            }
            OnPropertyChanged(nameof(Sells));
        }
    }
}
