using CommunityToolkit.Mvvm.Messaging.Messages;
using XInstructor.Common.Models;

namespace XInstructor.Common.Messages;

public class NewNetworkOperation(OperationModel ops) : 
    ValueChangedMessage<OperationModel>(ops)
{
}
