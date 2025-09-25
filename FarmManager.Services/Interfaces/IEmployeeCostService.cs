using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IEmployeeCostService
{
    Task<ICollection<EmployeeCost>> GetAll(bool activeOnly = true);
    Task<EmployeeCost> Get(int id);
    Task Add(EmployeeCost entity);
    Task Update(EmployeeCost entity);
    Task Delete(int id);
}
