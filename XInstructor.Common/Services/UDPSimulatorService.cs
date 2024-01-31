using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using XInstructor.Common.Messages;
using XInstructor.Common.Models;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Services;

public class UDPSimulatorService
/// <summary>
///     Send and receive data with simulator on ports :
///         - 50555 (simulator inbound) 
///         - 50556 (simulator outbound)
/// </summary>
{
    private Socket? _emiterSocket;
    //private Socket? _receiverSocket;

    private EndPoint _remoteEP = new IPEndPoint(IPAddress.Any, 0);
    private byte[] _buffer = new byte[4*1024];

    private static Mutex ServiceMutex = new Mutex();
    public bool IsRunning { get; set; } = false;

    public bool Initalize()
    {
        _emiterSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //_receiverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        try
        {
            _emiterSocket.Bind(new IPEndPoint(IPAddress.Any, 50556));
        }
        catch (SocketException se)
        {
            Debug.WriteLine(se);
            return false;
        }
        ServiceMutex.WaitOne();
        IsRunning = true;
        ServiceMutex.ReleaseMutex();
        return true;
    }

    public void Start()
    {
        StartReceive();
    }

    public void Stop()
    {

    }

    private void StartReceive()
    {
        _emiterSocket!.BeginReceiveFrom(_buffer, 0, 500, SocketFlags.None, ref _remoteEP, EndReceive, _emiterSocket);
    }

    private void EndReceive(IAsyncResult ar)
    {
        int received;
        try
        {
            received = (ar.AsyncState as Socket)!.EndReceiveFrom(ar, ref _remoteEP);
        }
        catch (SocketException se)
        {
            Debug.WriteLine(se);
            IsRunning = false;
            return;
        }
        if (received > 0)
        {
            string data = Encoding.ASCII.GetString(_buffer, 0, received);
            Debug.WriteLine($"Received {data}");
            NetworkOperation? ops = null;
            try
            {
                ops = JsonSerializer.Deserialize<NetworkOperation>(data);
                ops.OperationData = data;
            }
            catch (JsonException je)
            {
                Debug.WriteLine($"Message was {data}");
                Debug.WriteLine($"JSON Exception {je}");
            }
            if (ops != null)
            {
                WeakReferenceMessenger.Default.Send(new NetworkOperationReceived(ops));
            }
        }
        bool needRerun = false;
        ServiceMutex.WaitOne();
        needRerun = IsRunning;
        ServiceMutex.ReleaseMutex();
        if (needRerun) StartReceive();
    }


    public int SendData(string data, IPEndPoint target)
    {
        if (_emiterSocket is null) return -2;
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        int res = -1;
        try
        {
           res = _emiterSocket.SendTo(buffer, target);
        }
        catch(SocketException se)
        {
            Debug.WriteLine(se);
            Debugger.Break();
        }
        return res;
    }

    public int SendData(string data, ClientModel client)
    {
        return SendData(data, new IPEndPoint(client.RemoteAddress, client.RemotePort));
    }
}
