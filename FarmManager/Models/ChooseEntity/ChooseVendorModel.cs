using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.ChooseEntity;

public class ChooseVendorModel
{
    public ObservableCollection<Vendor> Vendors { get; set; }
    public Vendor SelectedItem { get; set; }
}
