using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Seasons;
using FarmManager.App.Views;
using FarmManager.App.Views.Seasons;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonEditViewModel(ISeasonService seasonService) : BaseViewModel
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
        get { return Model.Season.Plant?.Name; }
        set
        {
            if (Model.Season.Plant != null)
                Model.Season.Plant.Name = value ?? string.Empty;
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
        Model.Season.StartDate = DateTime.SpecifyKind(Model.Season.StartDate.Date, DateTimeKind.Utc);
        Model.Season.EndDate = DateTime.SpecifyKind(Model.Season.EndDate.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

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
        var window = new SeasonChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Season.Plant = window.Plant;
            OnPropertyChanged(nameof(Plant));
        }
    }
}
