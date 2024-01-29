using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using XInstructor.UI.Models;

namespace XInstructor.UI.ViewModels;

public partial class WeatherWindViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<WindLayerModel> _windLayers = [];

    [ObservableProperty]
    private WindLayerModel? _selectedWindLayer;


    partial void OnSelectedWindLayerChanged(WindLayerModel? value)
    {
        WindLayers = new(WindLayers.OrderBy(w => w.Altitude));
    }


    [RelayCommand]
    private void DeleteWindLayer(WindLayerModel? model)
    {
        if (model == null) return;
        WindLayers.Remove(model);
        WindLayers = new(WindLayers.OrderBy(w => w.Altitude));
    }

    [RelayCommand]
    private void AddWindLayer()
    {
        WindLayers.Add(new() { Altitude = 500, DirectionTrue = 330, VariableFrom = 300, VariableTo = 010, MaxSpeed = 15, MinSpeed = 10, Speed = 10 });
        WindLayers = new(WindLayers.OrderBy(w => w.Altitude));
    }


    public WeatherWindViewModel()
    {
#if DEBUG
        WindLayers = [
            new WindLayerModel() { Altitude = 500, DirectionTrue = 330, VariableFrom = 300, VariableTo = 010, MaxSpeed = 15, MinSpeed = 10, Speed = 10 },
            new WindLayerModel() { Altitude = 1500, DirectionTrue = 330, VariableFrom = 300, VariableTo = 010, MaxSpeed = 15, MinSpeed = 10, Speed = 10 },
            new WindLayerModel() { Altitude = 3000, DirectionTrue = 330, VariableFrom = 300, VariableTo = 010, MaxSpeed = 15, MinSpeed = 10, Speed = 10 },
            new WindLayerModel() { Altitude = 5000, DirectionTrue = 330, VariableFrom = 300, VariableTo = 010, MaxSpeed = 15, MinSpeed = 10, Speed = 10 },
        ];
#endif
    }
}
