using XInstructor.Common.ViewModels;

namespace XInstructor.UI.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}