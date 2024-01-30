using XInstructor.UI.ViewModels;

namespace XInstructor.UI.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage(WeatherViewModelMaui vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}