using XInstructor.UI.ViewModels;

namespace XInstructor.UI.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage(WeatherViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}