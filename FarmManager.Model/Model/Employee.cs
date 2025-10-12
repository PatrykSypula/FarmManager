using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Employee : BaseEntity, IContactable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? IdNumber { get; set; }
    public bool IsRentable { get; set; } = false;
    public decimal? BaseRent { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
