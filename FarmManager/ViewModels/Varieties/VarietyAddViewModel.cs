using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Varieties;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Varieties;

public class VarietyAddViewModel(IVarietyService varietyService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Variety>? RequestClose;
    public VarietyAddModel Model = new VarietyAddModel();

    public string Name
    {
        get
        {
            return Model.Variety.Name;
        }
        set
        {
            Model.Variety.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Variety.Description;
        }
        set
        {
            Model.Variety.Description = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddVarietyAsync());

    private async Task AddVarietyAsync()
    {
        VarietyValidator validator = new VarietyValidator();
        Model.Variety.Description = string.IsNullOrEmpty(Model.Variety.Description) ? null : Model.Variety.Description;
        var result = validator.Validate(Model.Variety);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await varietyService.Add(Model.Variety);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Variety);

        }
    }
}
