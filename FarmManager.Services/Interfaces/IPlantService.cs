using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IPlantService
{
    Task<ICollection<Plant>> GetAll(bool activeOnly = true);
    Task<Plant> Get(int id);
    Task Add(Plant entity);
    Task Update(Plant entity);
    Task Delete(int id);
    Task<decimal> GetQuantity(int plantId);
}
