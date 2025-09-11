using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Diseases;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Diseases;

public class DiseaseEditViewModel(IDiseaseService diseaseService) : BaseViewModel
{
    #region Properties

    public event Action<Disease>? RequestClose;
    public DiseaseEditModel Model = new DiseaseEditModel();

    public string Name
    {
        get
        {
            return Model.Disease.Name;
        }
        set
        {
            Model.Disease.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Disease.Description;
        }
        set
        {
            Model.Disease.Description = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get
        {
            return Model.Disease.IsActive;
        }
        set
        {
            Model.Disease.IsActive = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Disease = await diseaseService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteDiseaseAsync());
    private async Task DeleteDiseaseAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę chorobę?").ShowDialog();
        if (result == true)
        {
            await diseaseService.Delete(Model.Disease.Id);
            Model.Disease.IsDeleted = true;
            RequestClose?.Invoke(Model.Disease);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateDiseaseAsync());
    private async Task UpdateDiseaseAsync()
    {
        DiseaseValidator validator = new DiseaseValidator();
        Model.Disease.Description = string.IsNullOrEmpty(Model.Disease.Description) ? null : Model.Disease.Description;
        var result = validator.Validate(Model.Disease);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await diseaseService.Update(Model.Disease);
            RequestClose?.Invoke(Model.Disease);
        }
    }
}
