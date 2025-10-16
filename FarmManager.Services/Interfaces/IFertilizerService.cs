using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;
public interface IFertilizerService
{
    Task<ICollection<Fertilizer>> GetAll(bool activeOnly = true);
    Task<Fertilizer> Get(int id);
    Task Add(Fertilizer entity);
    Task Update(Fertilizer entity);
    Task Delete(int id);
    Task<decimal> GetAvailableQuantity(int fertilizerId);
}
