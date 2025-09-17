using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Plants;
using FarmManager.App.Views;
using FarmManager.App.Views.Plants;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Plants;

public class PlantAddViewModel(IPlantService plantServive) : BaseViewModel
{
    #region Properties

    public event Action<Plant>? RequestClose;
    public PlantAddModel Model = new PlantAddModel();

    public string Name
    {
        get
        {
            return Model.Plant.Name;
        }
        set
        {
            Model.Plant.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Variety
    {
        get { return Model.Variety?.Name; }
        set
        {
            if (Model.Variety != null)
                Model.Variety.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Plant.Description;
        }
        set
        {
            Model.Plant.Description = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddPlantAsync());

    private async Task AddPlantAsync()
    {
        PlantValidator validator = new PlantValidator();
        Model.Plant.Description = string.IsNullOrEmpty(Model.Plant.Description) ? null : Model.Plant.Description;
        var result = validator.Validate(Model.Plant);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await plantServive.Add(Model.Plant);
            RequestClose?.Invoke(Model.Plant);

        }
    }

    public RelayCommand OpenVariety => new RelayCommand(execute => OpenSelectVarietyAsync());
    private void OpenSelectVarietyAsync()
    {
        var window = new PlantChooseVarietyWindow();
        if (window.ShowDialog() == true && window.Variety != null)
        {
            Model.Variety = window.Variety;
            Model.Plant.VarietyId = window.Variety.Id;
            OnPropertyChanged(nameof(Variety));
        }
    }
}
