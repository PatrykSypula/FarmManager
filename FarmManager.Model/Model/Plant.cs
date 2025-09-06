using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Plant : BaseEntity, IDescribable
{
    public Variety Variety { get; set; } = new Variety();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
