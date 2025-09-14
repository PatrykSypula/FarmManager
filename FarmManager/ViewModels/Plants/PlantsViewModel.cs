using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Plants;
using FarmManager.App.Views.Plants;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Plants;

public class PlantsViewModel(IPlantService plantService) : BaseViewModel
{
    #region Properties

    public PlantsModel Model = new PlantsModel();

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
        Plants = new ObservableCollection<Plant>(await plantService.GetAll(false));
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

    public RelayCommand Create => new RelayCommand(execute => OpenPlantAddWindow());
    private void OpenPlantAddWindow()
    {
        var window = new PlantAddWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Plants.Add(window.Plant);
            OnPropertyChanged(nameof(Plants));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenPlantEditWindow());
    private void OpenPlantEditWindow()
    {
        var window = new PlantEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Plant != null)
        {
            var plant = window.Plant;
            var index = Plants.ToList().FindIndex(d => d.Id == plant.Id);

            if (index >= 0)
            {
                if (plant.IsDeleted)
                {
                    Plants.RemoveAt(index);
                }
                else
                {
                    Plants.RemoveAt(index);
                    Plants.Insert(index, plant);
                }
            }
            OnPropertyChanged(nameof(Plants));
        }
    }
}
