using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Deposits;

public class DepositsModel
{
    public ObservableCollection<Deposit> Deposits { get; set; }
    public Deposit SelectedItem { get; set; }
}
