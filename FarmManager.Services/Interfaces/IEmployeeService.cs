using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IEmployeeService
{
    Task<ICollection<Employee>> GetAll(bool activeOnly = true);
    Task<Employee> Get(int id);
    Task Add(Employee entity);
    Task Update(Employee entity);
    Task Delete(int id);
    Task<ICollection<Employee>> GetActiveForWorkday(IEnumerable<int> ids);
}
