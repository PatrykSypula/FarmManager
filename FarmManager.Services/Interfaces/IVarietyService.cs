using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IVarietyService
{
    Task<ICollection<Variety>> GetAll(bool activeOnly = true);
    Task<Variety> Get(int id);
    Task Add(Variety entity);
    Task Update(Variety entity);
    Task Delete(int id);
}
