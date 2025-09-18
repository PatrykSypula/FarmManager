using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseFertilizerViewModel(IFertilizerService fertilizerService) : BaseViewModel
{
    #region Properties

    public event Action<Fertilizer>? RequestClose;

    public ChooseFertilizerModel Model = new ChooseFertilizerModel();

    public ObservableCollection<Fertilizer> Fertilizers
    {
        get { return Model.Fertilizers; }
        set
        {
            Model.Fertilizers = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Fertilizers = new ObservableCollection<Fertilizer>(await fertilizerService.GetAll());
    }

    public Fertilizer SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Select => new RelayCommand(execute => SelectPlant());
    private void SelectPlant()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
