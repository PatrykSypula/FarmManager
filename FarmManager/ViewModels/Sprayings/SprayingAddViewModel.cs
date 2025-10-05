using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Sprayings;

public class SprayingAddViewModel(ISprayingService sprayingService, IFertilizerService fertilizerService, IBuyService buyService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Spraying>? RequestClose;
    public SprayingAddModel Model = new SprayingAddModel();

    public string? Plant
    {
        get
        {
            return Model.Plant.Name;
        }
        set
        {
            if (Model.Plant != null)
                Model.Plant.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public string? Fertilizer
    {
        get { return Model.Fertilizer?.Name; }
        set
        {
            if (Model.Fertilizer != null)
                Model.Fertilizer.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public DateTimeOffset Date
    {
        get
        {
            return Model.Spraying.Date;
        }
        set
        {
            Model.Spraying.Date = value;
            OnPropertyChanged();
        }
    }
    public double Quantity
    {
        get
        {
            return Model.Spraying.Quantity;
        }
        set
        {
            Model.Spraying.Quantity = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Spraying.Description;
        }
        set
        {
            Model.Spraying.Description = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddSprayingAsync());

    private async Task AddSprayingAsync()
    {
        SprayingValidator validator = new SprayingValidator();
        Model.Spraying.Description = string.IsNullOrEmpty(Model.Spraying.Description) ? null : Model.Spraying.Description;
        var result = validator.Validate(Model.Spraying);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            SprayingRegisterValidator fertilizerValidator = new SprayingRegisterValidator(Model.Spraying.Quantity);
            var valid = fertilizerValidator.Validate(Model.Fertilizer);
            if (!valid.IsValid)
            {
                new CustomMessageBoxOk(valid).ShowDialog();
            }
            else
            {
                Model.Spraying.BuyQuantity = await buyService.AdjustRemainingQuantity(Model.Spraying.Quantity, Model.Fertilizer.Id);
                //await fertilizerService.AddQuantity(Model.Fertilizer.Id, -Model.Spraying.Quantity);
                await sprayingService.Add(Model.Spraying);
                await unitOfWork.SaveChangesAsync();
                RequestClose?.Invoke(Model.Spraying);
            }
        }
    }

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new ChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Plant = window.Plant;
            Model.Spraying.PlantId = window.Plant.Id;
            OnPropertyChanged(nameof(Plant));
        }
    }

    public RelayCommand OpenFertilizer => new RelayCommand(execute => OpenSelectFertilizerAsync());
    private void OpenSelectFertilizerAsync()
    {
        var window = new ChooseFertilizerWindow();
        if (window.ShowDialog() == true && window.Fertilizer != null)
        {
            Model.Fertilizer = window.Fertilizer;
            Model.Spraying.FertilizerId = window.Fertilizer.Id;
            OnPropertyChanged(nameof(Fertilizer));
        }
    }
}
