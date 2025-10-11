using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IPaymentService
{
    Task<ICollection<Payment>> GetAll(bool activeOnly = true);
    Task<Payment> Get(int id);
    Task Add(Payment entity);
    Task Delete(int id);
}
