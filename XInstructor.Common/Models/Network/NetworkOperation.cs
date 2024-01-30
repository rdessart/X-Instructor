using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

public class NetworkOperation
{
    [JsonPropertyName("Operation")]
    public string OperationId { get; set; } = null!;
}
