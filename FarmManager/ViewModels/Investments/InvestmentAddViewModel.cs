using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Investments;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Investments;

public class InvestmentAddViewModel(IInvestmentService investmentService, IUnitOfWork unitOfWork) : BaseViewModel
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
    public async Task InitializeAsync()
    {
        Model.Investment.Date = DateOnly.FromDateTime(DateTime.Now);
        OnPropertyChanged(nameof(Date));
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddInvestmentAsync());

    private async Task AddInvestmentAsync()
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
            await investmentService.Add(Model.Investment);
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
}

