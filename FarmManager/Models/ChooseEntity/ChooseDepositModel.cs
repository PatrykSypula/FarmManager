using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;

public class ChooseDepositModel
{
    public ObservableCollection<Deposit> Deposits { get; set; }
    public Deposit SelectedItem { get; set; }
}
