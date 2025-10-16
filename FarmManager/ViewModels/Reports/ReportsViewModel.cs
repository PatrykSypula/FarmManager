using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Reports;
using FarmManager.App.Views.Reports;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Reports;

public class ReportsViewModel(IReportService reportService) : BaseViewModel
{
    #region Properties

    public ReportsModel Model = new ReportsModel();

    public ObservableCollection<Season> Seasons
    {
        get { return Model.Seasons; }
        set
        {
            Model.Seasons = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Seasons = new ObservableCollection<Season>(await reportService.GetSeasons());
        OnPropertyChanged(nameof(Seasons));
    }

    public Season SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Open => new RelayCommand(execute => OpenReportWindow());
    private void OpenReportWindow()
    {
        new ReportWindow(SelectedItem.Id).ShowDialog();
    }
}
