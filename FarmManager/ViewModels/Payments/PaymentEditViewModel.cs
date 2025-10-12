using FarmManager.App.Helpers;
using FarmManager.App.Models.Payments;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Payments;

public class PaymentEditViewModel(IPaymentService paymentService, IEmployeeService employeeService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Payment>? RequestClose;
    public PaymentEditModel Model = new PaymentEditModel();

    public string Employee
    {
        get
        {
            return Model.Employee.FirstName + " " + Model.Employee.LastName;
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

    public async Task InitializeAsync(int id)
    {
        Model.Payment = await paymentService.Get(id);
        Model.Employee = await employeeService.Get(Model.Payment.EmployeeId);
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Employee));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(EmployeeCost));
        OnPropertyChanged(nameof(PaymentQuantity));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteSprayingAsync());
    private async Task DeleteSprayingAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę wypłatę?").ShowDialog();
        if (result == true)
        {
            await paymentService.RevertPayment(Model.Payment.WorkdayQuantity);
            await paymentService.RevertPayEmployeeCosts(Model.Payment.EmployeeCosts);
            await paymentService.Delete(Model.Payment.Id);
            Model.Payment.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Payment);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateSprayingAsync());
    private async Task UpdateSprayingAsync()
    {
        new CustomMessageBoxOk("Edycja wypłaty jest obecnie niedostępna ze względu na złożoność związaną z zużywaniem zakupionego produktu.\nWszekie poprawki należy rozwiązywać dodawaniem kolejnych zakupów lub ich usuwaniem.").ShowDialog();
    }
}
