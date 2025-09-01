using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model
{
    public class EmployeeCost : BaseEntity
    {
        public Employee Employee { get; set; } = null!;
        public double Quantity { get; set; }
    }
}
