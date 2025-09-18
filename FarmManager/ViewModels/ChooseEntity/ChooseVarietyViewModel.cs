using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseVarietyViewModel(IVarietyService varietyService) : BaseViewModel
{
    #region Properties

    public event Action<Variety>? RequestClose;

    public ChooseVarietyModel Model = new ChooseVarietyModel();

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
        Varieties = new ObservableCollection<Variety>(await varietyService.GetAll());
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

    public RelayCommand Select => new RelayCommand(execute => SelectVariety());
    private void SelectVariety()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
