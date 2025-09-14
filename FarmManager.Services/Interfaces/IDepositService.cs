using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDepositService
{
    Task<ICollection<Deposit>> GetAll(bool activeOnly = true);
    Task<Deposit> Get(int id);
    Task Add(Deposit entity);
    Task Update(Deposit entity);
    Task Delete(int id);
}
