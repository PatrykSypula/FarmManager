
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.EmployeeCosts;
using FarmManager.App.Models.Investments;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Investments;
public class InvestmentEditViewModel(IInvestmentService investmentService, IPlantService plantService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Investment>? RequestClose;
    public InvestmentAddModel Model = new InvestmentAddModel();

    public string Name
    {
        get { return Model.Investment.Name; }
        set
        {
            Model.Investment.Name = value;
            OnPropertyChanged();
        }
    }

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

    public decimal Price
    {
        get { return Model.Investment.Price; }
        set
        {
            Model.Investment.Price = value;
            OnPropertyChanged();
        }
    }
    public DateOnly Date
    {
        get
        {
            return Model.Investment.Date;
        }
        set
        {
            Model.Investment.Date = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Investment.Description;
        }
        set
        {
            Model.Investment.Description = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Investment = await investmentService.Get(id);
        if(Model.Investment.PlantId != null)
        {
            Model.Plant = await plantService.Get(Model.Investment.PlantId.Value);
        }
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteInvestmentAsync());
    private async Task DeleteInvestmentAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę inwestycję?").ShowDialog();
        if (result == true)
        {
            await investmentService.Delete(Model.Investment.Id);
            Model.Investment.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Investment);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateInvestmentAsync());
    private async Task UpdateInvestmentAsync()
    {
        InvestmentValidator validator = new InvestmentValidator();
        Model.Investment.Description = string.IsNullOrEmpty(Model.Investment.Description) ? null : Model.Investment.Description;
        var result = validator.Validate(Model.Investment);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await investmentService.Update(Model.Investment);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Investment);
        }
    }

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new ChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Plant = window.Plant;
            Model.Investment.PlantId = window.Plant.Id;
            OnPropertyChanged(nameof(Plant));
        }
    }

    public RelayCommand RemovePlant => new RelayCommand(async execute => await RemovePlantAsync());
    private async Task RemovePlantAsync()
    {
        Model.Plant = new Plant();
        Model.Investment.PlantId = null;
        OnPropertyChanged(nameof(Plant));
    }
}
