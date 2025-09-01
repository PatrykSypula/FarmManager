using FarmManager.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Model
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeCost> EmployeeCosts { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Spraying> Sprayings { get; set; }
        public DbSet<Variety> Varietys { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<WorkDayCollecting> WorkDayCollecting { get; set; }
        public DbSet<WorkDayHourly> WorkDayHourly { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
