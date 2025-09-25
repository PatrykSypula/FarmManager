using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Seasons;
using FarmManager.App.Views.Seasons;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Seasons;

public class SeasonsViewModel(ISeasonService seasonService) : BaseViewModel
{
    #region Properties

    public SeasonsModel Model = new SeasonsModel();

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
        Seasons = new ObservableCollection<Season>(await seasonService.GetAll(false));
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

    public RelayCommand Create => new RelayCommand(execute => OpenSeasonAddWindow());
    private void OpenSeasonAddWindow()
    {
        var window = new SeasonAddWindow();
        if (window.ShowDialog() == true && window.Season != null)
        {
            Seasons.Add(window.Season);
            OnPropertyChanged(nameof(Seasons));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenSeasonEditWindow());
    private void OpenSeasonEditWindow()
    {
        var window = new SeasonEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Season != null)
        {
            var season = window.Season;
            var index = Seasons.ToList().FindIndex(d => d.Id == season.Id);

            if (index >= 0)
            {
                if (season.IsDeleted)
                {
                    Seasons.RemoveAt(index);
                }
                else
                {
                    Seasons.RemoveAt(index);
                    Seasons.Insert(index, season);
                }
            }
            OnPropertyChanged(nameof(Seasons));
        }
    }
}
