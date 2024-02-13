using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Models;

public class FailureModel
{
    public DatarefModel? Dataref { get; set; }
    public string OnValue { get; set; } = "1";
    public string OffValue { get; set; } = "0";
    public bool IsActivated { get; set; } = false;
}
