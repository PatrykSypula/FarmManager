using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDiseaseService
{
    Task<ICollection<Disease>> GetAll();
    Task<Disease> Get(int id);
    Task Add(Disease disease);
    Task Update(Disease disease);
    Task Delete(int id);
}
