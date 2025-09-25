using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Fertilizers;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Fertilizers;

public class FertilizerAddViewModel(IFertilizerService fertilizerService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Fertilizer>? RequestClose;
    public FertilizerAddModel Model = new FertilizerAddModel();

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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddFertilizerAsync());

    private async Task AddFertilizerAsync()
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
            await fertilizerService.Add(Model.Fertilizer);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Fertilizer);

        }
    }
}
