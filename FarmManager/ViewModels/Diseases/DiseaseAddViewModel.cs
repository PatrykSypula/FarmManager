using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Diseases;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Diseases;

public class DiseaseAddViewModel(IDiseaseService diseaseService) : BaseViewModel
{
    #region Properties

    public event Action<Disease>? RequestClose;
    public DiseaseAddModel Model = new DiseaseAddModel();

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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddDiseaseAsync());

    private async Task AddDiseaseAsync()
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
            await diseaseService.Add(Model.Disease);
            RequestClose?.Invoke(Model.Disease);

        }
    }
}
