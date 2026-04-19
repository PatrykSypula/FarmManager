using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Payments;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmManager.App.ViewModels.Payments;
public class PaymentAddViewModel(IPaymentService paymentService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Payment>? RequestClose;
    public PaymentAddModel Model = new PaymentAddModel();

    public string Employee
    {
        get
        {
            return string.IsNullOrWhiteSpace(Model.Employee.Nickname)
                ? $"{Model.Employee.FirstName} {Model.Employee.LastName}"
                : $"{Model.Employee.FirstName} {Model.Employee.Nickname} {Model.Employee.LastName}";
        }
        set
        {
            if (Model.Employee != null)
                Model.Employee.FirstName = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public decimal Quantity
    {
        get { return Model.Payment.Quantity; }
        set
        {
            Model.Payment.Quantity = value;
            OnPropertyChanged();
        }
    }
    public decimal EmployeeCost
    {
        get { return Model.Payment.EmployeeCost; }
        set
        {
            Model.Payment.EmployeeCost = value;
            OnPropertyChanged();
        }
    }
    public decimal PaymentQuantity
    {
        get { return Model.Payment.PaymentQuantity; }
        set
        {
            Model.Payment.PaymentQuantity = value;
            OnPropertyChanged();
        }
    }
    public decimal RentCost
    {
        get { return Model.Payment.RentCost; }
        set
        {
            Model.Payment.RentCost = value;
            OnPropertyChanged();
        }
    }
    public DateOnly Date
    {
        get
        {
            return Model.Payment.Date;
        }
        set
        {
            Model.Payment.Date = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Payment.Description;
        }
        set
        {
            Model.Payment.Description = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync()
    {
        Model.Payment.Date = DateOnly.FromDateTime(DateTime.Now);
        OnPropertyChanged(nameof(Date));
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddSprayingAsync());

    private async Task AddSprayingAsync()
    {
        PaymentValidator validator = new PaymentValidator();
        Model.Payment.Description = string.IsNullOrEmpty(Model.Payment.Description) ? null : Model.Payment.Description;
        var result = validator.Validate(Model.Payment);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            Model.Payment.WorkdayQuantity = await paymentService.PayAllWorkdays(Model.Payment.EmployeeId);
            Model.Payment.EmployeeCosts = await paymentService.GetEmployeeCostIds(Model.Payment.EmployeeId);
            await paymentService.Add(Model.Payment);
            await paymentService.PayEmployeeCosts(Model.Payment.EmployeeCosts);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Payment);
        }
    }

    public RelayCommand OpenEmployee => new RelayCommand(execute => OpenSelectEmployeeAsync());
    private async void OpenSelectEmployeeAsync()
    {
        var window = new ChooseEmployeeWindow();
        if (window.ShowDialog() == true && window.Employee != null)
        {
            Model.Employee = window.Employee;
            Model.Payment.EmployeeId = window.Employee.Id;
            Model.Payment.Quantity = await paymentService.GetUnpaidEmployeeQuantity(window.Employee.Id);
            Model.Payment.EmployeeCost = await paymentService.GetEmployeeCost(window.Employee.Id);
            Model.Payment.PaymentQuantity = Model.Payment.Quantity - Model.Payment.EmployeeCost;
            Model.Payment.RentCost = await paymentService.GetRentTotal(window.Employee.Id);
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(EmployeeCost));
            OnPropertyChanged(nameof(PaymentQuantity));
            OnPropertyChanged(nameof(Employee));
            OnPropertyChanged(nameof(RentCost));
        }
    }
}
