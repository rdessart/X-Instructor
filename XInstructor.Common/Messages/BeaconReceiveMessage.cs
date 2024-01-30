using CommunityToolkit.Mvvm.Messaging.Messages;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Messages;

public class BeaconReceiveMessage(BeaconOperation value) : 
    ValueChangedMessage<BeaconOperation>(value)
{
}
