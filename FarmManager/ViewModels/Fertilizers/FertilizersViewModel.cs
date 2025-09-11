using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Fertilizers;
using FarmManager.App.Views.Diseases;
using FarmManager.App.Views.Fertilizers;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Fertilizers;

public class FertilizersViewModel(IFertilizerService fertilizerService) : BaseViewModel
{
    #region Properties

    public FertilizersModel Model = new FertilizersModel();

    public ObservableCollection<Fertilizer> Fertilizers
    {
        get { return Model.Fertilizers; }
        set
        {
            Model.Fertilizers = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Fertilizers = new ObservableCollection<Fertilizer>(await fertilizerService.GetAll());
    }

    public Fertilizer SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenFertilizerAddWindow());
    private void OpenFertilizerAddWindow()
    {
        var window = new FertilizerAddWindow();
        if (window.ShowDialog() == true && window.Fertilizer != null)
        {
            Fertilizers.Add(window.Fertilizer);
            OnPropertyChanged(nameof(Fertilizers));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenFertilizerEditWindow());
    private void OpenFertilizerEditWindow()
    {
        var window = new FertilizerEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Fertilizer != null)
        {
            var fertilizer = window.Fertilizer;
            var index = Fertilizers.ToList().FindIndex(d => d.Id == fertilizer.Id);

            if (index >= 0)
            {
                if (fertilizer.IsDeleted)
                {
                    Fertilizers.RemoveAt(index);
                }
                else
                {
                    Fertilizers.RemoveAt(index);
                    Fertilizers.Insert(index, fertilizer);
                }
            }
            OnPropertyChanged(nameof(Fertilizers));
        }
    }
}
