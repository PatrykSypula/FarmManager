using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Seasons;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Seasons;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonEditViewModel(ISeasonService seasonService, IPlantService plantService) : BaseViewModel
{
    #region Properties

    public event Action<Season>? RequestClose;
    public SeasonEditModel Model = new SeasonEditModel();

    public string Name
    {
        get
        {
            return Model.Season.Name;
        }
        set
        {
            Model.Season.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Season.Description;
        }
        set
        {
            Model.Season.Description = value;
            OnPropertyChanged();
        }
    }
    public string? Plant
    {
        get { return Model.Plant?.Name; }
        set
        {
            if (Model.Plant != null)
                Model.Plant.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public DateTimeOffset StartDate
    {
        get
        {
            return Model.Season.StartDate;
        }
        set
        {
            Model.Season.StartDate = value;
            OnPropertyChanged();
        }
    }
    public DateTimeOffset EndDate
    {
        get
        {
            return Model.Season.EndDate;
        }
        set
        {
            Model.Season.EndDate = value.AddDays(1).AddTicks(-1);
            OnPropertyChanged();
        }
    }
    public bool IsActive
    {
        get
        {
            return Model.Season.IsActive;
        }
        set
        {
            Model.Season.IsActive = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Season = await seasonService.Get(id);
        Model.Plant = await plantService.Get(Model.Season.PlantId);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(StartDate));
        OnPropertyChanged(nameof(EndDate));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteSeasonAsync());
    private async Task DeleteSeasonAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten sezon?").ShowDialog();
        if (result == true)
        {
            await seasonService.Delete(Model.Season.Id);
            Model.Season.IsDeleted = true;
            RequestClose?.Invoke(Model.Season);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateSeasonAsync());
    private async Task UpdateSeasonAsync()
    {
        SeasonValidator validator = new SeasonValidator();
        Model.Season.Description = string.IsNullOrEmpty(Model.Season.Description) ? null : Model.Season.Description;
        var result = validator.Validate(Model.Season);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await seasonService.Update(Model.Season);
            RequestClose?.Invoke(Model.Season);
        }
    }

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new ChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Plant = window.Plant;
            Model.Season.PlantId = window.Plant.Id;
            OnPropertyChanged(nameof(Plant));
        }
    }
}
