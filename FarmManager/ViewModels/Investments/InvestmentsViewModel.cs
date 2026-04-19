using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Investments;
using FarmManager.App.Views.Investments;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Investments;
public class InvestmentsViewModel(IInvestmentService investmentService) : BaseViewModel
{
    #region Properties

    public InvestmentsModel Model = new InvestmentsModel();

    public ObservableCollection<Investment> Investments
    {
        get { return Model.Investments; }
        set
        {
            Model.Investments = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Investments = new ObservableCollection<Investment>(await investmentService.GetAll(false));
    }

    public Investment SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenInvestmentAddWindow());
    private async void OpenInvestmentAddWindow()
    {
        var window = new InvestmentAddWindow();
        if (window.ShowDialog() == true && window.Investment != null)
        {
            var investment = await investmentService.Get(window.Investment.Id);
            Investments.Add(investment);
            OnPropertyChanged(nameof(Investments));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenInvestmentEditWindow());
    private async void OpenInvestmentEditWindow()
    {
        var window = new InvestmentEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Investment != null)
        {
            var investment = new Investment();
            if (window.Investment.IsDeleted)
            {
                investment = window.Investment;
            }
            else
            {
                investment = await investmentService.Get(window.Investment.Id);
            }
            var index = Investments.ToList().FindIndex(d => d.Id == investment.Id);

            if (index >= 0)
            {
                if (investment.IsDeleted)
                {
                    Investments.RemoveAt(index);
                }
                else
                {
                    Investments.RemoveAt(index);
                    Investments.Insert(index, investment);
                }
            }
            OnPropertyChanged(nameof(Investments));
        }
    }
}
