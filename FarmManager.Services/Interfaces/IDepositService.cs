using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IDepositService
{
    Task AddDeposit(Deposit deposit);
    Task EditDeposit(Deposit deposit);
    Task DeleteDeposit(Guid depositId);
}
