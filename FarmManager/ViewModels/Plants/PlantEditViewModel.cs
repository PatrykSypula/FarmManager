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

public class PlantEditViewModel(IPlantService plantServive, IUnitOfWork unitOfWork) : BaseViewModel
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
    public decimal? Quantity
    {
        get
        {
            return Model.Plant.Quantity;
        }
        set
        {
            Model.Plant.Quantity = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Plant = await plantServive.Get(id);
        Model.Plant.Quantity = await plantServive.GetQuantity(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
        OnPropertyChanged(nameof(Quantity));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeletePlantAsync());
    private async Task DeletePlantAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tą roślinę?").ShowDialog();
        if (result == true)
        {
            var deletionResult = await plantServive.Delete(Model.Plant.Id);
            if (deletionResult.DidDelete)
            {
                Model.Plant.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();
                RequestClose?.Invoke(Model.Plant);
            }
            else
            {
                new CustomMessageBoxOk(deletionResult.Message).ShowDialog();
            }
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
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Plant);
        }
    }
}
