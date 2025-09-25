using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using FarmManager.App.Helpers;
using FarmManager.App.Models.EmployeeCosts;
using FarmManager.App.Views.EmployeeCosts;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.EmployeeCosts;

public class EmployeeCostsViewModel(IEmployeeCostService employeeCostService) : BaseViewModel
{
    #region Properties

    public EmployeeCostsModel Model = new EmployeeCostsModel();

    public ObservableCollection<EmployeeCost> EmployeeCosts
    {
        get { return Model.EmployeeCosts; }
        set
        {
            Model.EmployeeCosts = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        EmployeeCosts = new ObservableCollection<EmployeeCost>(await employeeCostService.GetAll(false));
    }

    public EmployeeCost SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenEmployeeCostAddWindow());
    private async void OpenEmployeeCostAddWindow()
    {
        var window = new EmployeeCostAddWindow();
        if (window.ShowDialog() == true && window.EmployeeCost != null)
        {
            var employeeCost = await employeeCostService.Get(window.EmployeeCost.Id);
            EmployeeCosts.Add(employeeCost);
            OnPropertyChanged(nameof(EmployeeCosts));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenEmployeeCostEditWindow());
    private async void OpenEmployeeCostEditWindow()
    {
        var window = new EmployeeCostEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.EmployeeCost != null)
        {
            var employeeCost = new EmployeeCost();
            if (window.EmployeeCost.IsDeleted)
            {
                employeeCost = window.EmployeeCost;
            }
            else
            {
                employeeCost = await employeeCostService.Get(window.EmployeeCost.Id);
            }
            var index = EmployeeCosts.ToList().FindIndex(d => d.Id == employeeCost.Id);

            if (index >= 0)
            {
                if (employeeCost.IsDeleted)
                {
                    EmployeeCosts.RemoveAt(index);
                }
                else
                {
                    EmployeeCosts.RemoveAt(index);
                    EmployeeCosts.Insert(index, employeeCost);
                }
            }
            OnPropertyChanged(nameof(EmployeeCosts));
        }
    }
}
