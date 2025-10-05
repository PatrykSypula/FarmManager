using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Models.Actions;

public class ActionEditModel
{
    public Action Action { get; set; } = new Action();
}
