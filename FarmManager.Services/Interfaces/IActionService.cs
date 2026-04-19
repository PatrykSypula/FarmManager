using FarmManager.Model.Exceptions;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.Services.Interfaces;

public interface IActionService
{
    Task<ICollection<Action>> GetAll(bool activeOnly = true);
    Task<Action> Get(int id);
    Task Add(Action entity);
    Task Update(Action entity);
    Task<DeletionResult> Delete(int id);
}
