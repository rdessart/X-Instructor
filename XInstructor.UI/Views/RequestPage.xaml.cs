using XInstructor.Common.ViewModels;

namespace XInstructor.UI.Views;

public partial class RequestPage : ContentPage
{
	public RequestPage(RequestViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}