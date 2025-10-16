using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseActionViewModel(IActionService actionService) : BaseViewModel
{
    #region Properties

    public event Action<Action>? RequestClose;

    public ChooseActionModel Model = new ChooseActionModel();

    public ObservableCollection<Action> Actions
    {
        get { return Model.Actions; }
        set
        {
            Model.Actions = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Actions = new ObservableCollection<Action>(await actionService.GetAll());
    }

    public Action SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Select => new RelayCommand(execute => SelectAction());
    private void SelectAction()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
