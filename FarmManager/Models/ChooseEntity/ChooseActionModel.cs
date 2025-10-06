using System.Collections.ObjectModel;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Models.ChooseEntity;
public class ChooseActionModel
{
    public ObservableCollection<Action> Actions { get; set; }
    public Action SelectedItem { get; set; }
}
