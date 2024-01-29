using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using XInstructor.UI.Models;
using XInstructor.UI.ViewModels.Weather;
using XInstructor.UI.Views.Weather;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace XInstructor.UI.ViewModels;

public partial class WeatherViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<TabModel> _tabs = [];

    [ObservableProperty]
    private TabModel _selectedTab;

    [ObservableProperty]
    private ContentView? _content;

    partial void OnSelectedTabChanged(TabModel value)
    {
        if (value == null) return;
        Type pageType = Type.GetType(value.ViewModel.GetType().FullName!.Replace("ViewModel", "View")) ?? throw new NullReferenceException("Unable to find related view");
        if (!(Activator.CreateInstance(pageType) is ContentView page)) return;
        page.BindingContext = value.ViewModel;
        Content = page;
    }

    public WeatherViewModel()
    {
#if DEBUG
        Tabs = [
            new("General", new WeatherGeneralViewModel()),
            new("Date & Time", new WeatherTimeViewModel()),
            new("Wind", new WeatherWindViewModel()),
            new("Cloud", new WeatherCloudViewModel()),
        ];

        SelectedTab = Tabs[2];
#endif
    }
}
