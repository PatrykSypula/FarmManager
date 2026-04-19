using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Actions;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.ViewModels.Actions;
public class ActionEditViewModel(IActionService actionService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Action>? RequestClose;
    public ActionEditModel Model = new ActionEditModel();

    public string Name
    {
        get
        {
            return Model.Action.Name;
        }
        set
        {
            Model.Action.Name = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Action.Description;
        }
        set
        {
            Model.Action.Description = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get
        {
            return Model.Action.IsActive;
        }
        set
        {
            Model.Action.IsActive = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Action = await actionService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteActionAsync());
    private async Task DeleteActionAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę czynność?").ShowDialog();
        if (result == true)
        {
            var deletionResult = await actionService.Delete(Model.Action.Id);
            if (deletionResult.DidDelete)
            {
                Model.Action.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();
                RequestClose?.Invoke(Model.Action);
            }
            else
            {
                new CustomMessageBoxOk(deletionResult.Message).ShowDialog();
            }
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateActionAsync());
    private async Task UpdateActionAsync()
    {
        ActionValidator validator = new ActionValidator();
        Model.Action.Description = string.IsNullOrEmpty(Model.Action.Description) ? null : Model.Action.Description;
        var result = validator.Validate(Model.Action);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await actionService.Update(Model.Action);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Action);
        }
    }
}
