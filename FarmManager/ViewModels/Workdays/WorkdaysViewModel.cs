using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FarmManager.App.Helpers;
using FarmManager.App.Views;

namespace FarmManager.App.ViewModels.Workdays;

public class WorkdaysViewModel : BaseViewModel
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
        if(execute is DateTime date)
        {
            new CustomMessageBoxOk(date.ToString("dd.MM.yyyy")).ShowDialog();
        }
    }

    public WorkdaysViewModel()
    {
        BuildCalendar();
    }

    private void ChangeMonth(int delta)
    {
        SelectedMonth = SelectedMonth.AddMonths(delta);
        BuildCalendar();
        OnPropertyChanged(nameof(CurrentMonthName));
    }

    private static readonly Random _random = new();

    private void BuildCalendar()
    {
        Days.Clear();

        var firstOfMonth = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);

        int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // first Monday
        var startDate = firstOfMonth.AddDays(-offset);

        int totalDays = offset + daysInMonth;
        Rows = (int)Math.Ceiling(totalDays / 7.0);

        for (int i = 0; i < Rows * 7; i++)
        {
            var day = startDate.AddDays(i);
            var events = new List<SchedulerEvent>();

            if (day.Month == SelectedMonth.Month)
            {
                int numberOfEvents = _random.Next(0, 4); // 0–3 events
                for (int e = 1; e <= numberOfEvents; e++)
                {
                    bool isPaid = _random.Next(0, 2) == 1; // random paid/unpaid
                    events.Add(new SchedulerEvent
                    {
                        Text = "Pryskanie",
                        IsPaid = isPaid
                    });
                }
            }

            Days.Add(new SchedulerDay
            {
                Date = day,
                IsCurrentMonth = day.Month == SelectedMonth.Month,
                Events = events
            });
        }

        OnPropertyChanged(nameof(Rows));
    }
}
