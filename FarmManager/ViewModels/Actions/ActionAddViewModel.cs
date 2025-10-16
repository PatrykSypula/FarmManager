using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Actions;
using FarmManager.App.Views;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.ViewModels.Actions;

public class ActionAddViewModel(IActionService actionService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Action>? RequestClose;
    public ActionAddModel Model = new ActionAddModel();

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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddActionAsync());

    private async Task AddActionAsync()
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
            await actionService.Add(Model.Action);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Action);
        }
    }
}
