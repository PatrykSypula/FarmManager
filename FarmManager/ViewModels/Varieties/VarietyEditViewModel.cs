using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Varieties;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Varieties;

public class VarietyEditViewModel(IVarietyService varietyService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Variety>? RequestClose;
    public VarietyEditModel Model = new VarietyEditModel();

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

    public bool IsActive
    {
        get
        {
            return Model.Variety.IsActive;
        }
        set
        {
            Model.Variety.IsActive = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Variety = await varietyService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteVarietyAsync());
    private async Task DeleteVarietyAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę odmianę?").ShowDialog();
        if (result == true)
        {
            await varietyService.Delete(Model.Variety.Id);
            Model.Variety.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Variety);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateVarietyAsync());
    private async Task UpdateVarietyAsync()
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
            await varietyService.Update(Model.Variety);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Variety);
        }
    }
}
