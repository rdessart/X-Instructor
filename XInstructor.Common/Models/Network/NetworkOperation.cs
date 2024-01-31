using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

[JsonPolymorphic]
[JsonDerivedType(typeof(SpeakOperation))]
public class NetworkOperation
{
    [JsonPropertyName("Operation")]
    public string OperationName { get; set; }

    [JsonPropertyName("OperationId")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public uint OperationId { get; set; }

    [JsonPropertyName("Result")]
    public string? OperationResult { get; set; }

    [JsonIgnore]
    public string? OperationData { get; set; }

    public NetworkOperation() : this("")
    {
    }

    public NetworkOperation(string operation)
    {
        OperationName = operation;
    }
}
