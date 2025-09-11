using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IVendorService
{
    Task<ICollection<Vendor>> GetAll();
    Task<Vendor> Get(int id);
    Task Add(Vendor entity);
    Task Update(Vendor entity);
    Task Delete(int id);
}
