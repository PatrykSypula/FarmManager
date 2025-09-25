using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;

public class ChooseFertilizerModel
{
    public ObservableCollection<Fertilizer> Fertilizers { get; set; }
    public Fertilizer SelectedItem { get; set; }
}
