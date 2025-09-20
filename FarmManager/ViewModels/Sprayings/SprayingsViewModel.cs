using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views.Sprayings;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Sprayings;

public class SprayingsViewModel(ISprayingService sprayingService) : BaseViewModel
{
    #region Properties

    public SprayingsModel Model = new SprayingsModel();

    public ObservableCollection<Spraying> Sprayings
    {
        get { return Model.Sprayings; }
        set
        {
            Model.Sprayings = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Sprayings = new ObservableCollection<Spraying>(await sprayingService.GetAll(false));
    }

    public Spraying SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenSprayingAddWindow());
    private async void OpenSprayingAddWindow()
    {
        var window = new SprayingAddWindow();
        if (window.ShowDialog() == true && window.Spraying != null)
        {
            var spraying = await sprayingService.Get(window.Spraying.Id);
            Sprayings.Add(spraying);
            OnPropertyChanged(nameof(Sprayings));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenSprayingEditWindow());
    private void OpenSprayingEditWindow()
    {
        var window = new SprayingEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Spraying != null)
        {
            var spraying = window.Spraying;
            var index = Sprayings.ToList().FindIndex(d => d.Id == spraying.Id);

            if (index >= 0)
            {
                if (spraying.IsDeleted)
                {
                    Sprayings.RemoveAt(index);
                }
                else
                {
                    Sprayings.RemoveAt(index);
                    Sprayings.Insert(index, spraying);
                }
            }
            OnPropertyChanged(nameof(Sprayings));
        }
    }
}
