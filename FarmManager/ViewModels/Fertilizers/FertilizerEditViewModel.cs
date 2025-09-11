using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Fertilizers;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Fertilizers;

public class FertilizerEditViewModel(IFertilizerService fertilizerService) : BaseViewModel
{
    public event Action<Fertilizer>? RequestClose;
    public FertilizerEditModel Model = new FertilizerEditModel();

    #region Properties
    public string Name
    {
        get
        {
            return Model.Fertilizer.Name;
        }
        set
        {
            Model.Fertilizer.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Fertilizer.Description;
        }
        set
        {
            Model.Fertilizer.Description = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get
        {
            return Model.Fertilizer.IsActive;
        }
        set
        {
            Model.Fertilizer.IsActive = value;
            OnPropertyChanged();
        }
    }
    #endregion


    public async Task InitializeAsync(int id)
    {
        Model.Fertilizer = await fertilizerService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteFertilizerAsync());
    private async Task DeleteFertilizerAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten nawóz?").ShowDialog();
        if (result == true)
        {
            await fertilizerService.Delete(Model.Fertilizer.Id);
            Model.Fertilizer.IsDeleted = true;
            RequestClose?.Invoke(Model.Fertilizer);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateFertilizerAsync());
    private async Task UpdateFertilizerAsync()
    {
        FertilizerValidator validator = new FertilizerValidator();
        Model.Fertilizer.Description = string.IsNullOrEmpty(Model.Fertilizer.Description) ? null : Model.Fertilizer.Description;
        var result = validator.Validate(Model.Fertilizer);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await fertilizerService.Update(Model.Fertilizer);
            RequestClose?.Invoke(Model.Fertilizer);
        }
    }
}
