using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace XInstructor.Common.ViewModels.Weather;

public partial class TimeViewModel : BaseViewModel
{
    [ObservableProperty]
    private DateTime _currentSystemTime = DateTime.Now;

    [ObservableProperty]
    private DateOnly _targetDate;

    [ObservableProperty]
    private TimeSpan _targetTime;

    [ObservableProperty]
    private DateTime _targetDateTime;

    [ObservableProperty]
    private DateTime? _simulatorDateTime;

    [ObservableProperty]
    private string? _timeSyncMode;

    [ObservableProperty]
    private string _seasonPreset;

    [ObservableProperty]
    private string _timePreset;

    partial void OnTimeSyncModeChanged(string? value)
    {
        Debug.WriteLine($"Time SyncMode is {value}");
        switch(value)
        {
            case "SystemSync":
                TargetDateTime = CurrentSystemTime;
                break;
            case "Manual":
                UpdateTime(TargetDate, TargetTime);
                break;
            case "ManualPreset":
                UpdateTime(SeasonPreset, TimePreset);
                break;
            default:
                break;
        }
    }

    partial void OnTargetTimeChanged(TimeSpan value)
    {
        if (TimeSyncMode != "Manual") return;
        UpdateTime(TargetDate, value);
    }

    partial void OnTargetDateChanged(DateOnly value)
    {
        if (TimeSyncMode != "Manual") return;
        UpdateTime(value, TargetTime);
    }

    private void UpdateTime(DateOnly date, TimeSpan time )
    {
        TargetDateTime = new(
            year: date.Year,
            month: date.Month,
            day: date.Day,
            hour: time.Hours,
            minute: time.Minutes,
            second: 00);
    }

    partial void OnSeasonPresetChanged(string value)
    {
        UpdateTime(value, TimePreset);
    }

    partial void OnTimePresetChanged(string value)
    {
        UpdateTime(SeasonPreset, value);
    }

    private void UpdateTime(string season, string timePreset)
    {
        DateOnly date = new DateOnly(CurrentSystemTime.Year, CurrentSystemTime.Month, CurrentSystemTime.Day);
        TimeSpan time = new TimeSpan();
        switch(season)
        {
            case "Spring":
                date = new DateOnly(CurrentSystemTime.Year, 04, 01);
                break;
            case "Summer":
                date = new DateOnly(CurrentSystemTime.Year, 08, 01);
                break;
            case "Fall":
                date = new DateOnly(CurrentSystemTime.Year, 10, 01);
                break;
            case "Winter":
                date = new DateOnly(CurrentSystemTime.Year, 01, 01);
                break;
            default:
                break;
        }

        switch(timePreset)
        {
            case "Morning":
                time = new TimeSpan(06, 00, 00);
                break;
            case "Noon":
                time = new TimeSpan(12, 00, 00);
                break;
            case "Evening":
                time = new TimeSpan(18, 00, 00);
                break;
            case "Night":
                time = new TimeSpan(22, 00, 00);
                break;
        }
        UpdateTime(date, time);
    }


    private System.Timers.Timer _timer;

    public TimeViewModel()
    {
        CurrentSystemTime = DateTime.Now;
        TargetDate = new DateOnly(CurrentSystemTime.Year, CurrentSystemTime.Month, CurrentSystemTime.Day);
        TargetTime = DateTime.Now.TimeOfDay;
        TimeSyncMode = "Off";
        SeasonPreset = "Summer";
        TimePreset = "Noon";
        _timer = new(1000) { AutoReset = true };
        _timer.Elapsed += (_, __) => CurrentSystemTime = DateTime.Now;
        _timer.Start();
    }
}
