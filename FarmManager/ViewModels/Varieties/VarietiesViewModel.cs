using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Varieties;
using FarmManager.App.Views.Varieties;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Varieties;

public class VarietiesViewModel(IVarietyService varietyService) : BaseViewModel
{
    #region Properties

    public VarietiesModel Model = new VarietiesModel();

    public ObservableCollection<Variety> Varieties
    {
        get { return Model.Varieties; }
        set
        {
            Model.Varieties = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Varieties = new ObservableCollection<Variety>(await varietyService.GetAll(false));
    }

    public Variety SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenVarietyAddWindow());
    private void OpenVarietyAddWindow()
    {
        var window = new VarietyAddWindow();
        if (window.ShowDialog() == true && window.Variety != null)
        {
            Varieties.Add(window.Variety);
            OnPropertyChanged(nameof(Varieties));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenVarietyEditWindow());
    private void OpenVarietyEditWindow()
    {
        var window = new VarietyEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Variety != null)
        {
            var variety = window.Variety;
            var index = Varieties.ToList().FindIndex(d => d.Id == variety.Id);

            if (index >= 0)
            {
                if (variety.IsDeleted)
                {
                    Varieties.RemoveAt(index);
                }
                else
                {
                    Varieties.RemoveAt(index);
                    Varieties.Insert(index, variety);
                }
            }
            OnPropertyChanged(nameof(Varieties));
        }
    }
}
