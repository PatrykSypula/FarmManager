using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Plants;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Plants;

public class PlantChooseVarietyViewModel(IVarietyService varietyService) : BaseViewModel
{
    #region Properties

    public event Action<Variety>? RequestClose;

    public PlantChooseVarietyModel Model = new PlantChooseVarietyModel();

    public ObservableCollection<Variety> Varieties
    {
        get { return Model.Varieties; }
        set
        {
            Model.Varieties = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Varieties = new ObservableCollection<Variety>(await varietyService.GetAll());
    }

    public Variety SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Select => new RelayCommand(execute => SelectVariety());
    private void SelectVariety()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
