using System.Collections.ObjectModel;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Models.Actions;

public class ActionsModel
{
    public ObservableCollection<Action> Actions { get; set; }
    public Action SelectedItem { get; set; }
}
