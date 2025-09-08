using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class DepositService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IDepositService
{
    public async Task<ICollection<Deposit>> GetAll()
    {
        return await context.Deposits.AsNoTracking().ToListAsync();
    }
    public async Task<Deposit> Get(int id)
    {
        return await context.Deposits.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć depozytu.");
    }
    public async Task Add(Deposit deposit)
    {
        await context.Deposits.AddAsync(deposit);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Deposit deposit)
    {
        var existingDeposit = context.Deposits.FirstOrDefault(d => d.Id == deposit.Id) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        existingDeposit.Name = deposit.Name;
        existingDeposit.PhoneNumber = deposit.PhoneNumber;
        existingDeposit.Email = deposit.Email;
        existingDeposit.Description = deposit.Description;
        existingDeposit.IsActive = deposit.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var deposit = context.Deposits.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        deposit.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
