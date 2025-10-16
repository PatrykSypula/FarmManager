using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Payments;

public class PaymentsModel
{
    public ObservableCollection<Payment> Payments { get; set; }
    public Payment SelectedItem { get; set; }
}
