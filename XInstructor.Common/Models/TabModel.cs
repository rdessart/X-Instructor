using XInstructor.Common.ViewModels;

namespace XInstructor.Common.Models;

public class TabModel(string header, BaseViewModel vm)
{
    public string Header { get; set; } = header;
    public BaseViewModel ViewModel { get; set; } = vm;
}
