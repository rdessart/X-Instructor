using CommunityToolkit.Mvvm.ComponentModel;

namespace XInstructor.Common.Models;

public partial class WindLayerModel : ObservableObject
{
    [ObservableProperty]
    private double _altitude;

    [ObservableProperty]
    private double _directionTrue;

    [ObservableProperty]
    private double _speed;

    [ObservableProperty]
    private double _variableFrom;

    [ObservableProperty]
    private double _variableTo;

    [ObservableProperty]
    private double _minSpeed;

    [ObservableProperty]
    private double _maxSpeed;
}
