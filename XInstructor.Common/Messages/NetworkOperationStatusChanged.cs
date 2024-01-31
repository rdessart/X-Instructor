using CommunityToolkit.Mvvm.Messaging.Messages;
using XInstructor.Common.Models;

namespace XInstructor.Common.Messages;

public class NetworkOperationStatusChanged(OperationModel ops) : 
    ValueChangedMessage<OperationModel>(ops)
{
}
