using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IInvestmentService
{
    Task<ICollection<Investment>> GetAll(bool activeOnly = true);
    Task<Investment> Get(int id);
    Task Add(Investment entity);
    Task Update(Investment entity);
    Task Delete(int id);
}
