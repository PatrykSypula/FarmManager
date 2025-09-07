using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Interfaces;

public class DepositService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IDepositService
{
    public async Task<ICollection<Deposit>> GetAll()
    {
        return await context.Deposits.ToListAsync();
    }
    public async Task<Deposit> Get(Guid guid)
    {
        return await context.Deposits.Where(d => d.Id == guid).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć depozytu.");
    }
    public async Task Add(Deposit deposit)
    {
        await context.Deposits.AddAsync(deposit);
        await unitOfWork.SaveChangesAsync();
    }

    public Task Delete(Guid depositId)
    {
        var deposit = context.Deposits.FirstOrDefault(d => d.Id == depositId) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        deposit.IsDeleted = true;
        return unitOfWork.SaveChangesAsync();
    }
    public Task Update(Deposit deposit)
    {
        var existingDeposit = context.Deposits.FirstOrDefault(d => d.Id == deposit.Id) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        existingDeposit.Name = deposit.Name;
        existingDeposit.Description = deposit.Description;
        existingDeposit.PhoneNumber = deposit.PhoneNumber;
        return unitOfWork.SaveChangesAsync();
    }
}
