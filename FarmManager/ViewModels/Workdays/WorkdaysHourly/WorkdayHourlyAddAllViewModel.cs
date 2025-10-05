using FarmManager.App.Helpers;
using FarmManager.App.Models.Workdays.WorkdaysCollecting;
using FarmManager.App.Models.Workdays.WorkdaysHourly;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysHourly;

public class WorkdayHourlyAddAllViewModel(IEmployeeService employeeService) : BaseViewModel
{
    #region Properties
    private string _fileHours = "HourlyValue.txt";
    private string _filePrice = "PriceValue.txt";

    public event Action<ICollection<WorkdayHourly>>? RequestClose;
    public WorkdayHourlyAddAllModel Model = new WorkdayHourlyAddAllModel();

    public double Price
    {
        get
        {
            return Model.Price;
        }
        set
        {
            Model.Price = value;
            OnPropertyChanged();
        }
    }
    public double Hours
    {
        get
        {
            return Model.Hours;
        }
        set
        {
            Model.Hours = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(IEnumerable<int> ids)
    {
        Model.Employees = await employeeService.GetActiveForWorkday(ids);
        var price = FileHelper.Read(_filePrice);
        if (!String.IsNullOrEmpty(price))
        {
            Model.Price = double.Parse(price);
            OnPropertyChanged(nameof(Price));
        }
        var hours = FileHelper.Read(_fileHours);
        if (!String.IsNullOrEmpty(hours))
        {
            Model.Hours = double.Parse(hours);
            OnPropertyChanged(nameof(Hours));
        }
    }

    #endregion



    public RelayCommand Add => new RelayCommand(async execute => await AddWorkdaysCollectingAsync());

    private async Task AddWorkdaysCollectingAsync()
    {
        if (new CustomMessageBoxYesNo($"Czy na pewno chcesz dodać każdego pracownika z {Model.Hours} godzinami i stawką {Model.Price}zł?").ShowDialog() == true)
        {
            foreach (var item in Model.Employees)
            {
                Model.WorkdaysHourly.Add(new WorkdayHourly
                {
                    EmployeeId = item.Id,
                    Price = Model.Price,
                    Hours = Model.Hours,
                    Employee = item
                });
            }
            FileHelper.Write(_filePrice, Model.Price.ToString());
            FileHelper.Write(_fileHours, Model.Hours.ToString());
            RequestClose?.Invoke(Model.WorkdaysHourly);
        }
    }
}
