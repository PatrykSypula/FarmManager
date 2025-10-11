using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class PaymentService(IFarmManagerContext context) : IPaymentService
{
    public async Task<ICollection<Payment>> GetAll(bool activeOnly = true)
    {
        IQueryable<Payment> query = context.Payments.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(s => s.Employee)
            .Include(s => s.WorkdayQuantity)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Payment> Get(int id)
    {
        return await context.Payments
            .AsNoTracking()
            .Include(s => s.Employee)
            .Include(s => s.WorkdayQuantity)
            .Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć wypłaty.");
    }
    public async Task Add(Payment entity)
    {
        context.Payments.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await context.Payments.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć wypłaty.");
        entity.IsDeleted = true;
    }
}
