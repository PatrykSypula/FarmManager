using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public bool IsRentable { get; set; } = false;
    public decimal? BaseRent { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
}
