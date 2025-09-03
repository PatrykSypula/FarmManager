using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? IdNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsRentable { get; set; } = false;
    public double BaseRent { get; set; }
}
