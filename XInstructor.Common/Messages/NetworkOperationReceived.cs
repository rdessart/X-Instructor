using CommunityToolkit.Mvvm.Messaging.Messages;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Messages;

public class NetworkOperationReceived(NetworkOperation value) :
    ValueChangedMessage<NetworkOperation>(value)
{
}
