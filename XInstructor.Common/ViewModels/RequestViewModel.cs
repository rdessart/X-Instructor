using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using XInstructor.Common.Messages;
using XInstructor.Common.Models;
using XInstructor.Common.Models.Network;
using XInstructor.Common.Services;

namespace XInstructor.Common.ViewModels;

public partial class RequestViewModel:
    BaseViewModel, IRecipient<NewNetworkOperation>, IRecipient<NetworkOperationStatusChanged>
{
    private readonly ClientManagerService _clientManagerService;

    [ObservableProperty]
    ObservableCollection<OperationModel> _operations = [];

    [RelayCommand]
    private void SendTextOperation()
    {
        _clientManagerService.BroadcastOperation(new SpeakOperation("Hello World!"));
    }

    public void Receive(NewNetworkOperation message)
    {
        Operations.Add(message.Value);
    }

    public void Receive(NetworkOperationStatusChanged message)
    {
        var op = Operations.First(o => o.Id == message.Value.Id);
        op.Status = message.Value.Status;
        op.SendedTimestamp = message.Value.SendedTimestamp;
        op.ClosedTimestamp = message.Value.ClosedTimestamp;
    }

    public RequestViewModel(ClientManagerService clientManagerService)
    {
        _clientManagerService = clientManagerService;
        WeakReferenceMessenger.Default.Register<NetworkOperationStatusChanged>(this);
        WeakReferenceMessenger.Default.Register<NewNetworkOperation>(this);
    }
}
