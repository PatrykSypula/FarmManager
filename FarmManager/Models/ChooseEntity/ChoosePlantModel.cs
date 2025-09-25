using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;
public class ChoosePlantModel
{
    public ObservableCollection<Plant> Plants { get; set; }
    public Plant SelectedItem { get; set; }
}
