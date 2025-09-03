namespace FarmManager.Model.Model.Base;

public abstract class DetailedEntity : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
