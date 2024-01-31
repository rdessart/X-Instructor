using System.Text.Json.Serialization;

namespace XInstructor.Common.Models.Network;

public class SpeakOperation: NetworkOperation
{
    [JsonPropertyName("Text")]
    public string Text { get; set; } = null!;

    public SpeakOperation() : this("")
    {

    }

    public SpeakOperation(string text) : base("Speak")
    {
        Text = text;
    }
}
