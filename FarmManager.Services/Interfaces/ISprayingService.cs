using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface ISprayingService
{
    Task<ICollection<Spraying>> GetAll(bool activeOnly = true);
    Task<Spraying> Get(int id);
    Task Add(Spraying entity);
    Task Update(Spraying entity);
    Task Delete(int id);
}
