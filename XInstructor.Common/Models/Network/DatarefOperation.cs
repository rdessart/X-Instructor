using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

public class DatarefOperation : NetworkOperation
{
    [JsonPropertyName("Dataref")]
    public DatarefModel Dataref { get; set; } = null!;
}
