namespace FarmManager.Model.Model.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
}
