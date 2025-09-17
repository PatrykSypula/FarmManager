using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Seasons;
using FarmManager.App.Views;
using FarmManager.App.Views.Plants;
using FarmManager.App.Views.Seasons;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonAddViewModel(ISeasonService seasonService) : BaseViewModel
{
    #region Properties

    public event Action<Season>? RequestClose;
    public SeasonAddModel Model = new SeasonAddModel();

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
    public string? Plant
    {
        get { return Model.Season.Plant?.Name; }
        set
        {
            if (Model.Season.Plant != null)
                Model.Season.Plant.Name = value ?? string.Empty;
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
    public DateTime StartDate
    {
        get
        {
            return Model.Season.StartDate;
        }
        set
        {
            Model.Season.StartDate = DateTime.SpecifyKind(value.Date, DateTimeKind.Utc);
            OnPropertyChanged();
        }
    }
    public DateTime EndDate
    {
        get
        {
            return Model.Season.EndDate;
        }
        set
        {
            Model.Season.EndDate = DateTime.SpecifyKind(value.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync()
    {
        Model.Season.StartDate = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);
        Model.Season.EndDate = DateTime.SpecifyKind(DateTime.UtcNow.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

        OnPropertyChanged(nameof(StartDate));
        OnPropertyChanged(nameof(EndDate));
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddSeasonAsync());

    private async Task AddSeasonAsync()
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
            await seasonService.Add(Model.Season);
            RequestClose?.Invoke(Model.Season);

        }
    }

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new SeasonChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Season.Plant = window.Plant;
            OnPropertyChanged(nameof(Plant));
        }
    }
}
