using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Vendors;

public class VendorsModel
{
    public ObservableCollection<Vendor> Vendors { get; set; }
    public Vendor SelectedItem { get; set; }
}
