using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Seasons;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonChoosePlantViewModel(IPlantService plantService) : BaseViewModel
{
    #region Properties

    public event Action<Plant>? RequestClose;

    public SeasonChoosePlantModel Model = new SeasonChoosePlantModel();

    public ObservableCollection<Plant> Plants
    {
        get { return Model.Plants; }
        set
        {
            Model.Plants = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Plants = new ObservableCollection<Plant>(await plantService.GetAll());
    }

    public Plant SelectedItem
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
