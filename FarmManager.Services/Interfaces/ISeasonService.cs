using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface ISeasonService
{
    Task<ICollection<Season>> GetAll(bool activeOnly = true);
    Task<Season> Get(int id);
    Task Add(Season entity);
    Task Update(Season entity);
    Task Delete(int id);
}
