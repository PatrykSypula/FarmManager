using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDepositService
{
    Task<ICollection<Deposit>> GetAll();
    Task<Deposit> Get(Guid guid);
    Task Add(Deposit deposit);
    Task Update(Deposit deposit);
    Task Delete(Guid depositId);
}
