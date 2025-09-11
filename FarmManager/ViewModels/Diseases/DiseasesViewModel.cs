using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Diseases;
using FarmManager.App.Views.Diseases;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Diseases;

public class DiseasesViewModel(IDiseaseService diseaseService) : BaseViewModel
{
    #region Properties

    public DiseasesModel Model = new DiseasesModel();

    public ObservableCollection<Disease> Diseases
    {
        get { return Model.Diseases; }
        set
        {
            Model.Diseases = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Diseases = new ObservableCollection<Disease>(await diseaseService.GetAll());
    }

    public Disease SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand CreateDisease => new RelayCommand(execute => OpenDiseaseAddWindow());
    private void OpenDiseaseAddWindow()
    {
        var window = new DiseaseAddWindow();
        if (window.ShowDialog() == true && window.Disease != null)
        {
            Diseases.Add(window.Disease);
            OnPropertyChanged(nameof(Diseases));
        }
    }

    public RelayCommand EditDisease => new RelayCommand(execute => OpenDiseaseEditWindow());
    private void OpenDiseaseEditWindow()
    {
        var window = new DiseaseEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Disease != null)
        {
            var disease = window.Disease;
            var index = Diseases.ToList().FindIndex(d => d.Id == disease.Id);

            if (index >= 0)
            {
                if (disease.IsDeleted)
                {
                    Diseases.RemoveAt(index);
                }
                else
                {
                    Diseases.RemoveAt(index);
                    Diseases.Insert(index, disease);
                }
            }
            OnPropertyChanged(nameof(Diseases));
        }
    }
}
