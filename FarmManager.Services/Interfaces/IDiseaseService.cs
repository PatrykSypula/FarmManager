using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDiseaseService
{
    Task<ICollection<Disease>> GetAll(bool activeOnly = true);
    Task<Disease> Get(int id);
    Task Add(Disease entity);
    Task Update(Disease entity);
    Task Delete(int id);
}
