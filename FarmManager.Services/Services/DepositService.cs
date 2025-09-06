using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;

namespace FarmManager.Services.Interfaces;

public class DepositService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IDepositService
{
    public async Task AddDeposit(Deposit deposit)
    {
        await context.Deposits.AddAsync(deposit);
        await unitOfWork.SaveChangesAsync();
    }

    public Task DeleteDeposit(Guid depositId)
    {
        var deposit = context.Deposits.FirstOrDefault(d => d.Id == depositId) ??
            throw new NotFoundException("Deposit cannot be found.");
        deposit.IsDeleted = true;
        return unitOfWork.SaveChangesAsync();
    }
    public Task EditDeposit(Deposit deposit)
    {
        var existingDeposit = context.Deposits.FirstOrDefault(d => d.Id == deposit.Id) ??
            throw new NotFoundException("Deposit cannot be found.");
        existingDeposit.Name = deposit.Name;
        existingDeposit.Description = deposit.Description;
        existingDeposit.PhoneNumber = deposit.PhoneNumber;
        return unitOfWork.SaveChangesAsync();
    }
}
