using System.Net;
using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

public class BeaconOperation : NetworkOperation
{
    [JsonPropertyName("Author")]
    public string AircraftAuthor { get; set; } = null!;

    [JsonPropertyName("Description")]
    public string AircraftDescription { get; set; } = null!;

    [JsonPropertyName("ListeningPort")]
    public int SimulatorInbound  { get; set; }

    [JsonPropertyName("EmitPort")]
    public int SimulatorOutbound  { get; set; }

    [JsonPropertyName("IPAddress")]
    public string SimulatorIp  { get; set; } = null!;

    [JsonPropertyName("Simulator")]
    public string SimulatorType { get; set; } = null!;

    [JsonPropertyName("SimulatorVersion")]
    public int SimulatorVersion { get; set; }

    [JsonPropertyName("SimulatorSDKVersion")]
    public int SimulatorSdkVersion { get; set; }

    [JsonPropertyName("Time")]
    public int SystemTime { get; set; }

    [JsonIgnore]
    public DateTime BeaconTimeStampZulu => DateTimeOffset.FromUnixTimeSeconds(SystemTime).DateTime;

    [JsonIgnore]
    public IPEndPoint RemoteEP => new IPEndPoint(IPAddress.Parse(SimulatorIp), SimulatorInbound);
    
    [JsonIgnore]
    public IPEndPoint LocalEP => new IPEndPoint(IPAddress.Parse(SimulatorIp), SimulatorOutbound);
}
