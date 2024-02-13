using System.Text.Json.Serialization;

namespace XInstructor.Common.Models;

public class NavigraphAiracResult
{
    [JsonPropertyName("Id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("Comments")]
    public string Comments { get; set; } = null!;

    [JsonPropertyName("Version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("Url")]
    public string Url { get; set; } = null!;
}
