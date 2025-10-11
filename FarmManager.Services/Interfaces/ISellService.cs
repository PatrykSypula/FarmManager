using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface ISellService
{
    Task<ICollection<Sell>> GetAll(bool activeOnly = true);
    Task<Sell> Get(int id);
    Task Add(Sell entity);
    Task Delete(int id);
}
