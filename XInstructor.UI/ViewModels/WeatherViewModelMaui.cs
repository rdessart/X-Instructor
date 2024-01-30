using CommunityToolkit.Mvvm.ComponentModel;
using XInstructor.Common.Models;

namespace XInstructor.UI.ViewModels;

public partial class WeatherViewModelMaui : XInstructor.Common.ViewModels.WeatherViewModel
{
    [ObservableProperty]
    private ContentView? _content;

    [ObservableProperty]
    private TabModel? _selectedTab;

    //partial void OnSelectedTabChanged(TabModel? oldValue, TabModel? value)
    partial void OnSelectedTabChanged(TabModel? oldValue, TabModel? newValue)
    {
        if (newValue == null || oldValue == newValue) return;
        Type pageType = Type.GetType(newValue.ViewModel.GetType().FullName!.Replace("ViewModel", "View").Replace("Common", "UI")) ?? throw new NullReferenceException("Unable to find related view");
        if (!(Activator.CreateInstance(pageType) is ContentView page)) return;
        page.BindingContext = newValue.ViewModel;
        Content = page;
    }

    public WeatherViewModelMaui() : base()
    {
        SelectedTab = Tabs.FirstOrDefault();
    }
}
