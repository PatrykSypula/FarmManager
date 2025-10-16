using System.Xml.Linq;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Reports;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Reports;

public class ReportViewModel(IReportService reportService) : BaseViewModel
{
    #region Properties

    public ReportModel Model = new ReportModel();

    public string SeasonName
    {
        get
        {
            return Model.Report.SeasonName;
        }
        set
        {
            Model.Report.SeasonName = value;
            OnPropertyChanged();
        }
    }
    public string PlantName
    {
        get
        {
            return Model.Report.PlantName;
        }
        set
        {
            Model.Report.PlantName = value;
            OnPropertyChanged();
        }
    }

    public DateOnly StartDate
    {
        get
        {
            return Model.Report.StartDate;
        }
        set
        {
            Model.Report.StartDate = value;
            OnPropertyChanged();
        }
    }
    public DateOnly EndDate
    {
        get
        {
            return Model.Report.EndDate;
        }
        set
        {
            Model.Report.EndDate = value;
            OnPropertyChanged();
        }
    }
    public decimal EmployeeEarnings
    {
        get
        {
            return Model.Report.EmployeeEarnings;
        }
        set
        {
            Model.Report.EmployeeEarnings = value;
            OnPropertyChanged();
        }
    }
    public decimal Rent
    {
        get
        {
            return Model.Report.Rent;
        }
        set
        {
            Model.Report.Rent = value;
            OnPropertyChanged();
        }
    }
    public decimal Income
    {
        get
        {
            return Model.Report.Income;
        }
        set
        {
            Model.Report.Income = value;
            OnPropertyChanged();
        }
    }
    public decimal SprayingCost
    {
        get
        {
            return Model.Report.SprayingCost;
        }
        set
        {
            Model.Report.SprayingCost = value;
            OnPropertyChanged();
        }
    }
    public decimal CountedIncome
    {
        get
        {
            return Model.Report.CountedIncome;
        }
        set
        {
            Model.Report.CountedIncome = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Report = await reportService.GenerateReport(id);
        OnPropertyChanged(nameof(SeasonName));
        OnPropertyChanged(nameof(PlantName));
        OnPropertyChanged(nameof(StartDate));
        OnPropertyChanged(nameof(EndDate));
        OnPropertyChanged(nameof(EmployeeEarnings));
        OnPropertyChanged(nameof(Rent));
        OnPropertyChanged(nameof(Income));
        OnPropertyChanged(nameof(SprayingCost));
        OnPropertyChanged(nameof(CountedIncome));
    }

    #endregion
}
