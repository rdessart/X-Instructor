using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using XInstructor.Common.Models;
using XInstructor.Common.ViewModels;

namespace XInstructor.Common.ViewModels;

public partial class WeatherViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<TabModel> _tabs = [];

    public WeatherViewModel()
    {
#if DEBUG
        Tabs = [
            new("General"    , new Weather.GeneralViewModel()),
            new("Date & Time", new Weather.TimeViewModel()),
            new("Wind"       , new Weather.WindViewModel()),
            new("Cloud"      , new Weather.CloudViewModel()),
        ];
#endif
    }
}
