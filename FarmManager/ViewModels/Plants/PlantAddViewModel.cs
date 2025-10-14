using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Plants;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Plants;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Plants;

public class PlantAddViewModel(IPlantService plantServive, IUnitOfWork unitOfWork) : BaseViewModel
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
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Plant);

        }
    }
}
