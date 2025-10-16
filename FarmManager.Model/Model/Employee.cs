using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public bool IsRentable { get; set; } = false;
    private decimal? _baseRent;
    public decimal? BaseRent{ get => _baseRent; set => _baseRent = value is null ? null : Math.Round(value.Value, 2); }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
}
