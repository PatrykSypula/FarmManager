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

public class WorkdaysCalendarViewModel(IWorkdayService workdayService, ISprayingService sprayingService) : BaseViewModel
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
    private void DayClickOpenWorkDay(object execute)
    {
        if (execute is DateTime dateTime)
        {
            var window = new WorkdaysWindow(DateOnly.FromDateTime(dateTime));

            window.Closed += (sender, args) =>
            {
                BuildCalendar();
            };

            window.ShowDialog();
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
        var sprayings = await sprayingService.GetSprayingsInMonth(SelectedMonth.Year, SelectedMonth.Month);

        var firstOfMonth = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);

        int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7;
        var startDate = firstOfMonth.AddDays(-offset);
        int totalDays = offset + daysInMonth;
        Rows = (int)Math.Ceiling(totalDays / 7.0);

        for (int i = 0; i < Rows * 7; i++)
        {
            var day = startDate.AddDays(i);

            var dayEvents = sprayings
                .Where(s => s.Date == DateOnly.FromDateTime(day))
                .Select(s => new SchedulerEvent
                {
                    Text = $"Pryskanie - {s.Plant?.Name}",
                    IsPaid = true
                })
                .ToList();

            dayEvents.AddRange(workdays
                .Where(w => w.Date == DateOnly.FromDateTime(day))
                .Select(w => new SchedulerEvent
                {
                    Text = GetEventText(w),
                    IsPaid = !HasDebt(w)
                }));

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
