using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IPaymentService
{
    Task<ICollection<Payment>> GetAll(bool activeOnly = true);
    Task<Payment> Get(int id);
    Task Add(Payment entity);
    Task Delete(int id);
    Task<decimal> GetUnpaidEmployeeQuantity(int employeeId);
    Task<decimal> GetEmployeeCost(int employeeId);
    Task<ICollection<int>> GetEmployeeCostIds(int employeeId);
    Task PayEmployeeCosts(ICollection<int> employeeCosts);
    Task RevertPayEmployeeCosts(ICollection<int> employeeCosts);
    Task<ICollection<PaymentWorkdayQuantity>> PayAllWorkdays(int employeeId);
    Task RevertPayment(ICollection<PaymentWorkdayQuantity> paymentWorkdayQuantities);
    Task<decimal> GetRentTotal(int employeeId);
}
