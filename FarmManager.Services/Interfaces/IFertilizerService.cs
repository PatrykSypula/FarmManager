using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;
public interface IFertilizerService
{
    Task<ICollection<Fertilizer>> GetAll();
    Task<Fertilizer> Get(int id);
    Task Add(Fertilizer entity);
    Task Update(Fertilizer entity);
    Task Delete(int id);
}
