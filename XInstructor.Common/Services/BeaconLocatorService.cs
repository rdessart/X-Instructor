namespace XInstructor.Common.Services;

public class BeaconLocatorService
/// <summary> 
/// Locate beacons calls (simulator) on broadcast port 50888
/// Beacon lenght will be less than 500 bytes.
/// Beacon structure : 
///     * Author               = Aircraft Author
///     * Description          = Aircraft Description
///     * EmitPort             = Simulator Outbound Port
///     * IPAddress            = Simulator IP Address
///     * ListeningPort        = Simulator Inboudn Port
///     * Operation            = Operation Type (should read 'beacon')
///     * Simulator            = Simulator Familly ('X-Plane', 'P3D', 'MSFS', 'FS') 
///     * SimulatorSDKVersion  = Simulator SDK Version
///     * SimulatorVersion     = Simulator Version (X-Plane : 12090 for X-Plane 12.0.9.0)
///     * Time                 = Beacon Emit Time (Simulator OS TIME when the beacon was emitted) 
/// summary>
{
}
