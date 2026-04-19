using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Investments;

public class InvestmentsModel
{
    public ObservableCollection<Investment> Investments { get; set; }
    public Investment SelectedItem { get; set; }
}
