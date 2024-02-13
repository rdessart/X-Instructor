using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

public class DatarefModel
{
    [JsonPropertyName("Link")]
    public string? Link { get; set; }
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    [JsonPropertyName("Type")]
    public string? Type { get; set; }
    [JsonPropertyName("Value")]
    public string? Value { get; set; }
}