using FarmManager.Model.Model;
using Microsoft.EntityFrameworkCore;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.Model.DatabaseContext;

public interface IFarmManagerContext
{
    public DbSet<Action> Actions { get; set; }
    public DbSet<Buy> Buys { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeCost> EmployeeCosts { get; set; }
    public DbSet<Fertilizer> Fertilizers { get; set; }
    public DbSet<Harvest> Harvests { get; set; }
    public DbSet<Investment> Investments{ get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentWorkdayQuantity> PaymentWorkdayQuantitys { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Sell> Sells { get; set; }
    public DbSet<SellHarvestQuantity> SellHarvestQuantitys { get; set; }
    public DbSet<Spraying> Sprayings { get; set; }
    public DbSet<SprayingBuyQuantity> SprayingBuyQuantitys { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Workday> Workdays { get; set; }
    public DbSet<WorkdayCollecting> WorkdayCollecting { get; set; }
    public DbSet<WorkdayHourly> WorkdayHourly { get; set; }
}
