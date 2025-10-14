using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Seasons;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonAddViewModel(ISeasonService seasonService, IUnitOfWork unitOfWork) : BaseViewModel
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
        get { return Model.Plant?.Name; }
        set
        {
            if (Model.Plant != null)
                Model.Plant.Name = value ?? string.Empty;
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
    public DateOnly StartDate
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
    public DateOnly EndDate
    {
        get
        {
            return Model.Season.EndDate;
        }
        set
        {
            Model.Season.EndDate = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync()
    {
        Model.Season.StartDate = DateOnly.FromDateTime(DateTime.Now);
        Model.Season.EndDate = DateOnly.FromDateTime(DateTime.Now);

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
            await unitOfWork.SaveChangesAsync();
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
