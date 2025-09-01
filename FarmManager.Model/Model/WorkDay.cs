using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model
{
    public class WorkDay : BaseEntity
    {
        public DateOnly Date { get; set; }
        public ICollection<WorkDayCollecting> WorkDayCollectings { get; set; } = [];
        public ICollection<WorkDayHourly> WorkDayHourly { get; set; } = [];
        public ICollection<Spraying> Spraying { get; set; } = [];
    }
}
