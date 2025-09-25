namespace FarmManager.Model.Model.Base;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
}
