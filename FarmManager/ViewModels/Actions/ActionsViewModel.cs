using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Actions;
using FarmManager.App.Views.Actions;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.ViewModels.Actions;
public class ActionsViewModel(IActionService actionService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public ActionsModel Model = new ActionsModel();

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
        Actions = new ObservableCollection<Action>(await actionService.GetAll(false));
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

    public RelayCommand Create => new RelayCommand(execute => OpenActionAddWindow());
    private void OpenActionAddWindow()
    {
        var window = new ActionAddWindow();
        if (window.ShowDialog() == true && window.Action != null)
        {
            Actions.Add(window.Action);
            OnPropertyChanged(nameof(Action));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenActionEditWindow());
    private void OpenActionEditWindow()
    {
        var window = new ActionEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Action != null)
        {
            var action = window.Action;
            var index = Actions.ToList().FindIndex(d => d.Id == action.Id);

            if (index >= 0)
            {
                if (action.IsDeleted)
                {
                    Actions.RemoveAt(index);
                }
                else
                {
                    Actions.RemoveAt(index);
                    Actions.Insert(index, action);
                }
            }
            OnPropertyChanged(nameof(Actions));
        }
    }
}
