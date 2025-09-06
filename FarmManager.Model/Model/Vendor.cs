using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Vendor : BaseEntity, IDescribable, IContactable
{
    public string? PhoneNumber { get ; set; }
    public string? Email { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
