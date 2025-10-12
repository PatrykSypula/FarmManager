using FarmManager.App.Helpers;
using FarmManager.App.Models.Workdays.WorkdaysCollecting;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;

public class WorkdayCollectingAddAllViewModel(IEmployeeService employeeService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties
    private string filename = "CollectingValue.txt";

    public event Action<ICollection<WorkdayCollecting>>? RequestClose;
    public WorkdayCollectingAddAllModel Model = new WorkdayCollectingAddAllModel();

    public decimal Price
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

    public async Task InitializeAsync(IEnumerable<int> ids)
    {
        Model.Employees = await employeeService.GetActiveForWorkday(ids);
        var price = FileHelper.Read(filename);
        if (!String.IsNullOrEmpty(price))
        {
            Model.Price = decimal.Parse(price);
            OnPropertyChanged(nameof(Price));
        }
    }

    #endregion



    public RelayCommand Add => new RelayCommand(async execute => await AddWorkdaysCollectingAsync());

    private async Task AddWorkdaysCollectingAsync()
    {
        if (new CustomMessageBoxYesNo($"Czy na pewno chcesz dodać każdego pracownika ze stawką {Model.Price}zł?").ShowDialog() == true)
        {
            foreach (var item in Model.Employees)
            {
                Model.WorkdaysCollecting.Add(new WorkdayCollecting
                {
                    EmployeeId = item.Id,
                    Price = Model.Price,
                    Employee = item
                });
            }
            FileHelper.Write(filename, Model.Price.ToString());
            RequestClose?.Invoke(Model.WorkdaysCollecting);
        }
    }
}
