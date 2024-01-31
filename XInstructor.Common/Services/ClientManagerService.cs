using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using System.Text.Json;
using XInstructor.Common.Messages;
using XInstructor.Common.Models;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Services;

public class ClientManagerService : IRecipient<NetworkOperationReceived>
{
    public ClientManagerService(UDPSimulatorService UdpSimulatorService)
    {
        _udpSimulatorService = UdpSimulatorService;
        WeakReferenceMessenger.Default.Register(this);
    }

    private readonly UDPSimulatorService _udpSimulatorService;

    private List<ClientModel> _activeClients { get; set; } = [];

    public List<OperationModel> OpenOperations { get; set; } = new();

    private uint _lastOperationId = 0;

    public int UpdateClientList(IEnumerable<ClientModel> clients)
    {
        _activeClients = new(clients);
        return _activeClients.Count;
    }
    
    public int AddClient(ClientModel client)
    {
        _activeClients.Add(client);
        return _activeClients.Count;
    }

    public int RemoveClient(ClientModel client)
    {
        _activeClients.Remove(client);
        return _activeClients.Count;
    }

    public IEnumerable<ClientModel> GetClient() => new List<ClientModel>(_activeClients);

    public void SendOperationToClient(NetworkOperation operation, ClientModel client)
    {
        _lastOperationId++;
        operation.OperationId = _lastOperationId;
        OperationModel opsModel = new(_lastOperationId, client, DateTime.Now);
        WeakReferenceMessenger.Default.Send(new NewNetworkOperation(opsModel));
        OpenOperations.Add(opsModel);
        var text = JsonSerializer.Serialize<NetworkOperation>(operation);
        int res = _udpSimulatorService.SendData(text, client);
        if (res <= 0) return;
        opsModel.Status = OperationModel.OperationStatus.Sent;
    }

    public void BroadcastOperation(NetworkOperation operation) 
    { 
        foreach(var client in _activeClients)
        {
            SendOperationToClient(operation, client);
        }
    }

    public void Receive(NetworkOperationReceived message)
    {
        DateTime receiptTime = DateTime.Now;
        OperationModel? ops = OpenOperations.FirstOrDefault(o => o.Id == message.Value.OperationId);
        if (ops == null) return;

        ops.Status = OperationModel.OperationStatus.Received;
        ops.ClosedTimestamp = receiptTime;
        if (message.Value.OperationResult == null) return;
        if(message.Value.OperationResult.ToUpper() == "OK")
        {
            Debug.WriteLine($"Operation {message.Value.OperationName} [{message.Value.OperationId}] executed sucessfully !");
            ops.Status = OperationModel.OperationStatus.Sucess;
        }
        else
        {
            ops.Status = OperationModel.OperationStatus.Failed;
            Debug.WriteLine($"Operation {message.Value.OperationName} [{message.Value.OperationId}] failed!\nReason : {message.Value.OperationResult}");
            Debugger.Break();
        }
    }
}

