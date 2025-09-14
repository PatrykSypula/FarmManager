using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Plants;
using FarmManager.App.Views;
using FarmManager.App.Views.Plants;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Plants;

public class PlantEditViewModel(IPlantService plantServive) : BaseViewModel
{
    #region Properties

    public event Action<Plant>? RequestClose;
    public PlantEditModel Model = new PlantEditModel();

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
    public string? Variety
    {
        get { return Model.Plant.Variety?.Name; }
        set
        {
            if (Model.Plant.Variety != null)
                Model.Plant.Variety.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public bool IsActive
    {
        get
        {
            return Model.Plant.IsActive;
        }
        set
        {
            Model.Plant.IsActive = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Plant = await plantServive.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Variety));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeletePlantAsync());
    private async Task DeletePlantAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tą roślinę?").ShowDialog();
        if (result == true)
        {
            await plantServive.Delete(Model.Plant.Id);
            Model.Plant.IsDeleted = true;
            RequestClose?.Invoke(Model.Plant);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdatePlantAsync());
    private async Task UpdatePlantAsync()
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
            await plantServive.Update(Model.Plant);
            RequestClose?.Invoke(Model.Plant);
        }
    }

    public RelayCommand OpenVariety => new RelayCommand(execute => OpenSelectVarietyAsync());
    private void OpenSelectVarietyAsync()
    {
        var window = new PlantChooseVarietyWindow();
        if (window.ShowDialog() == true && window.Variety != null)
        {
            Model.Plant.Variety = window.Variety;
            OnPropertyChanged(nameof(Variety));
        }
    }
}
