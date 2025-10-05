using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IHarvestService
{
    Task<ICollection<Harvest>> GetAll(bool activeOnly = true);
    Task<Harvest> Get(int id);
    Task<int> Add(Harvest entity);
    Task Update(Harvest entity);
    Task Delete(int id);
}
