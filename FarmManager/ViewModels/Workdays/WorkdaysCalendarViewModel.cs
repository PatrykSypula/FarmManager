using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FarmManager.App.Helpers;
using FarmManager.App.Views;
using FarmManager.App.Views.Workdays;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;
using Microsoft.Windows.Themes;

namespace FarmManager.App.ViewModels.Workdays;

public class WorkdaysCalendarViewModel(IWorkdayService workdayService) : BaseViewModel
{
    public ObservableCollection<SchedulerDay> Days { get; } = new();
    public string CurrentMonthName => char.ToUpper(SelectedMonth.ToString("MMMM yyyy")[0]) + SelectedMonth.ToString("MMMM yyyy").Substring(1);
    public int Rows { get; private set; } = 6;

    public DateTimeOffset SelectedMonth { get; private set; } = DateTimeOffset.UtcNow;

    public RelayCommand PreviousMonth => new RelayCommand(execute => ChangeMonth(-1));

    public RelayCommand NextMonth => new RelayCommand(execute => ChangeMonth(1));

    public RelayCommand CurrentMonth => new RelayCommand(execute => SetCurrentMonth());
    private void SetCurrentMonth()
    {
        SelectedMonth = DateTime.Today;
        BuildCalendar();
        OnPropertyChanged(nameof(CurrentMonthName));
    }

    public RelayCommand DayClick => new RelayCommand(execute => DayClickOpenWorkDay(execute));
    private async void DayClickOpenWorkDay(object execute)
    {
        if (execute is DateTime dateTime)
        {
            new WorkdaysWindow(DateOnly.FromDateTime(dateTime)).ShowDialog();
        }
    }

    public async Task InitializeAsync()
    {
        BuildCalendar();
    }

    private void ChangeMonth(int delta)
    {
        SelectedMonth = SelectedMonth.AddMonths(delta);
        BuildCalendar();
        OnPropertyChanged(nameof(CurrentMonthName));
    }

    private async void BuildCalendar()
    {
        Days.Clear();

        var workdays = await workdayService.GetWorkdaysInMonth(SelectedMonth.Year, SelectedMonth.Month);

        var firstOfMonth = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);

        int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // Monday-first
        var startDate = firstOfMonth.AddDays(-offset);
        int totalDays = offset + daysInMonth;
        Rows = (int)Math.Ceiling(totalDays / 7.0);

        for (int i = 0; i < Rows * 7; i++)
        {
            var day = startDate.AddDays(i);
            var dayEvents = workdays.Where(w => w.Date == DateOnly.FromDateTime(day))
                .Select(w => new SchedulerEvent
                {
                    Text = GetEventText(w),
                    IsPaid = !HasDebt(w)
                }).ToList();

            Days.Add(new SchedulerDay
            {
                Date = day,
                IsCurrentMonth = day.Month == SelectedMonth.Month,
                Events = dayEvents
            });
        }

        OnPropertyChanged(nameof(Rows));
    }
    private string GetEventText(Workday w)
    {
        string action = w.Action?.Name ?? "Rwanie";
        if (w.Plant?.Name != null)
            return $"{action} - {w.Plant.Name}";
        else
            return action;
    }

    private bool HasDebt(Workday w)
    {
        return w.WorkdaysCollecting.Any(c => c.RemainingToPay > 0) ||
               w.WorkdaysHourly.Any(h => h.RemainingToPay > 0);
    }
}
