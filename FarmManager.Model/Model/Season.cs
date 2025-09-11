using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Season : BaseEntity, IDescribable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Plant Plant { get; set; } = null!;
}
