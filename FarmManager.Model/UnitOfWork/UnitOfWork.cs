using FarmManager.Model.DatabaseContext;

namespace FarmManager.Model.UnitOfWork;

public class UnitOfWork(FarmManagerContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
