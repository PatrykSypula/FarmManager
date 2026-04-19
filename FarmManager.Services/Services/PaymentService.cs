using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class PaymentService(IFarmManagerContext context) : IPaymentService
{
    public async Task<ICollection<Payment>> GetAll(bool activeOnly = true)
    {
        IQueryable<Payment> query = context.Payments.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(s => s.Employee)
            .Include(s => s.WorkdayQuantity)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Payment> Get(int id)
    {
        return await context.Payments
            .AsNoTracking()
            .Include(s => s.Employee)
            .Include(s => s.WorkdayQuantity)
            .Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć wypłaty.");
    }
    public async Task Add(Payment entity)
    {
        context.Payments.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await context.Payments.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć wypłaty.");
        entity.IsDeleted = true;
    }

    public async Task<decimal> GetUnpaidEmployeeQuantity(int employeeId)
    {
        var workdaysCollecting = await context.WorkdayCollecting
            .Where(x => x.EmployeeId == employeeId && x.RemainingToPay != 0)
            .ToListAsync();
        var workdaysHourly = await context.WorkdayHourly
            .Where(x => x.EmployeeId == employeeId && x.RemainingToPay != 0)
            .ToListAsync();

        return workdaysCollecting.Sum(d => d.RemainingToPay) + workdaysHourly.Sum(d => d.RemainingToPay);
    }



    public async Task<ICollection<PaymentWorkdayQuantity>> PayAllWorkdays(int employeeId)
    {
        var workdaysCollecting = await context.WorkdayCollecting
            .Where(x => x.EmployeeId == employeeId && x.RemainingToPay != 0)
            .ToListAsync();

        var workdaysHourly = await context.WorkdayHourly
            .Where(x => x.EmployeeId == employeeId && x.RemainingToPay != 0)
            .ToListAsync();

        ICollection<PaymentWorkdayQuantity> adjustments = [];
        for (var i = 0; i < workdaysCollecting.Count; i++)
        {
            adjustments.Add(new PaymentWorkdayQuantity()
            {
                WorkdayCollectingId = workdaysCollecting[i].Id,
                Quantity = workdaysCollecting[i].RemainingToPay,
            });
            workdaysCollecting[i].RemainingToPay = 0;
        }

        for (var i = 0; i < workdaysHourly.Count; i++)
        {
            adjustments.Add(new PaymentWorkdayQuantity()
            {
                WorkdayHourlyId = workdaysHourly[i].Id,
                Quantity = workdaysHourly[i].RemainingToPay,
            });
            workdaysHourly[i].RemainingToPay = 0;
        }
        return adjustments;
    }
    public async Task RevertPayment(ICollection<PaymentWorkdayQuantity> paymentWorkdayQuantities)
    {
        foreach (var item in paymentWorkdayQuantities)
        {
            if(item.WorkdayCollectingId != null)
            {
                var workdayCollecting = await context.WorkdayCollecting.Where(w => w.Id == item.WorkdayCollectingId).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie można znaleźć dniówki zbiorczej.");
                workdayCollecting.RemainingToPay += item.Quantity;
                continue;
            }
            if(item.WorkdayHourlyId != null)
            {
                var workdayHourly = await context.WorkdayHourly.Where(w => w.Id == item.WorkdayHourlyId).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie można znaleźć dniówki godzinowej.");
                workdayHourly.RemainingToPay += item.Quantity;
                continue;
            }
        }
    }

    public async Task<decimal> GetEmployeeCost(int employeeId)
    {
        var employeeCosts = await context.EmployeeCosts
            .Where(x => x.EmployeeId == employeeId && x.IsPaid == false)
            .ToListAsync();

        return employeeCosts.Sum(d => d.Quantity);
    }

    public async Task<ICollection<int>> GetEmployeeCostIds(int employeeId)
    {
        return await context.EmployeeCosts
            .Where(x => x.EmployeeId == employeeId && x.IsPaid == false)
            .Select(x => x.Id)
            .ToListAsync();
    }
    public async Task PayEmployeeCosts(ICollection<int> employeeCosts)
    {
        foreach (var id in employeeCosts)
        {
            var employeeCost = await context.EmployeeCosts.Where(e => e.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie można znaleźć kosztu pracownika.");
            employeeCost.IsPaid = true;
        }
    }
    public async Task RevertPayEmployeeCosts(ICollection<int> employeeCosts)
    {
        foreach (var id in employeeCosts)
        {
            var employeeCost = await context.EmployeeCosts.Where(e => e.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie można znaleźć kosztu pracownika.");
            employeeCost.IsPaid = false;
        }
    }

    public async Task<decimal> GetRentTotal(int employeeId)
    {
        var employee = await context.Employees
            .FirstOrDefaultAsync(e => e.Id == employeeId)
            ?? throw new NotFoundException("Nie można znaleźć pracownika.");

        if (employee.IsRentable && employee.BaseRent != null)
        {
            var workdayIds = await context.Workdays
            .Where(w => w.IsActive &&
                (w.WorkdaysCollecting.Any(c => c.EmployeeId == employeeId) ||
                 w.WorkdaysHourly.Any(h => h.EmployeeId == employeeId)))
            .Select(w => w.Date)
            .Distinct()
            .ToListAsync();

            int uniqueWorkdayCount = workdayIds.Count;

            decimal rentPerDay = employee.BaseRent.Value;
            return uniqueWorkdayCount * rentPerDay;
        }
        else
        {
            return 0;
        }
    }
}
