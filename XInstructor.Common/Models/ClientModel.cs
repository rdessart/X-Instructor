using CommunityToolkit.Mvvm.ComponentModel;
using System.Net;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Models;

public partial class ClientModel : ObservableObject
{
    [ObservableProperty]
    private string _aircraftAuthor = string.Empty;

    [ObservableProperty]
    private string _aircraftDescription = string.Empty;

    [ObservableProperty]
    private string _simulator = string.Empty;

    [ObservableProperty]
    private DateTime _lastBeacon;

    [ObservableProperty]
    private IPAddress _remoteAddress = IPAddress.Any;

    [ObservableProperty]
    private int _remotePort = 0;

    public uint LastOperationId { get; set; } = 0;

    public void FromBeacon(BeaconOperation ops)
    {
        AircraftAuthor = ops.AircraftAuthor;
        AircraftDescription = ops.AircraftDescription;
        Simulator = $"{ops.SimulatorType} ({ops.SimulatorVersion})";
        LastBeacon = ops.BeaconTimeStampZulu.ToLocalTime();
        RemoteAddress = IPAddress.Parse(ops.SimulatorIp);
        RemotePort = ops.SimulatorInbound;
    }
}
