using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IVendorService
{
    Task<ICollection<Vendor>> GetAll(bool activeOnly = true);
    Task<Vendor> Get(int id);
    Task Add(Vendor entity);
    Task Update(Vendor entity);
    Task<DeletionResult> Delete(int id);
}
