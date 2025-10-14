using FarmManager.App.Helpers;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Sprayings;

public class SprayingEditViewModel(ISprayingService sprayingService, IFertilizerService fertilizerService, IPlantService plantService, IBuyService buyService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Spraying>? RequestClose;
    public SprayingEditModel Model = new SprayingEditModel();

    public string Plant
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
    public string Fertilizer
    {
        get { return Model.Fertilizer.Name; }
        set
        {
            if (Model.Fertilizer != null)
                Model.Fertilizer.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public DateOnly Date
    {
        get => Model.Spraying.Date;
        set
        {
            Model.Spraying.Date = value;
            OnPropertyChanged();
        }
    }
    public decimal Quantity
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

    public async Task InitializeAsync(int id)
    {
        Model.Spraying = await sprayingService.Get(id);
        Model.Plant = await plantService.Get(Model.Spraying.PlantId);
        Model.Fertilizer = await fertilizerService.Get(Model.Spraying.FertilizerId);
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(Fertilizer));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteSprayingAsync());
    private async Task DeleteSprayingAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć to pryskanie?").ShowDialog();
        if (result == true)
        {
            await buyService.RevertRemainingQuantity(Model.Spraying.BuyQuantity);
            await sprayingService.Delete(Model.Spraying.Id);
            Model.Spraying.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Spraying);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateSprayingAsync());
    private async Task UpdateSprayingAsync()
    {
        new CustomMessageBoxOk("Edycja zakupu jest obecnie niedostępna ze względu na złożoność związaną z zużywaniem zakupionego produktu.\nWszekie poprawki należy rozwiązywać dodawaniem kolejnych zakupów lub ich usuwaniem.").ShowDialog();
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
