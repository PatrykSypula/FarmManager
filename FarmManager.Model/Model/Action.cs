using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Action : BaseEntity, IDescribable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
