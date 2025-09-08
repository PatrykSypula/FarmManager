using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDepositService
{
    Task<ICollection<Deposit>> GetAll();
    Task<Deposit> Get(int id);
    Task Add(Deposit deposit);
    Task Update(Deposit deposit);
    Task Delete(int id);
}
