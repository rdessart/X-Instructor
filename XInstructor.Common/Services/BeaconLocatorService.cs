using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using XInstructor.Common.Messages;
using XInstructor.Common.Models.Network;

namespace XInstructor.Common.Services;

public class BeaconLocatorService
/// <summary> 
/// Locate beacons calls (simulator) on broadcast port 50888
/// Beacon lenght will be less than 500 bytes.
/// Beacon structure : 
///     * Author               = Aircraft Author
///     * Description          = Aircraft Description
///     * EmitPort             = Simulator Outbound Port
///     * IPAddress            = Simulator IP Address
///     * ListeningPort        = Simulator Inboudn Port
///     * Operation            = Operation Type (should read 'beacon')
///     * Simulator            = Simulator Familly ('X-Plane', 'P3D', 'MSFS', 'FS') 
///     * SimulatorSDKVersion  = Simulator SDK Version
///     * SimulatorVersion     = Simulator Version (X-Plane : 12090 for X-Plane 12.0.9.0)
///     * Time                 = Beacon Emit Time (Simulator OS TIME when the beacon was emitted) 
/// summary>
{
    private Socket _socket;
    private IPEndPoint _bindEndPoint;
    private EndPoint _remoteEP;
    private byte[] _buffer;

    private static Mutex ServiceMutex = new Mutex();

    public bool IsRunning { get; set; } = false;



    public BeaconLocatorService()
    {
        _remoteEP = new IPEndPoint(IPAddress.Any, 0);
        _bindEndPoint = new IPEndPoint(IPAddress.Any, 0);
        _buffer = new byte[500];
        _socket = new Socket(
           socketType: SocketType.Dgram,
           protocolType: ProtocolType.Udp,
           addressFamily: AddressFamily.InterNetwork);
    }

    public bool Initalise(int targetPort = 50888)
    {
        _socket = new Socket(
            socketType: SocketType.Dgram, 
            protocolType: ProtocolType.Udp, 
            addressFamily: AddressFamily.InterNetwork);

        _bindEndPoint.Port = targetPort;
        try
        {
            _socket.Bind(_bindEndPoint);
        }
        catch(SocketException se)
        {
            Debug.WriteLine(se);
            return false ;
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
        ServiceMutex.WaitOne();
        IsRunning = false;
        ServiceMutex.ReleaseMutex();
        _socket.Close();
    }

    protected void StartReceive()
    {
        _socket.BeginReceiveFrom(_buffer, 0, 500, SocketFlags.None, ref _remoteEP, EndReceive, _socket);
    }

    protected void EndReceive(IAsyncResult ar)
    {
        int received = 0;
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
            BeaconOperation? beacon = null;
            try
            {
                beacon = JsonSerializer.Deserialize<BeaconOperation>(data);
            }
            catch (JsonException je)
            {
                Debug.WriteLine($"Message was {data}");
                Debug.WriteLine($"JSON Exception {je}");
            }
            if (beacon != null)
            {
                WeakReferenceMessenger.Default.Send(new BeaconReceiveMessage(beacon));
            }
        }
        bool needRerun = false;
        ServiceMutex.WaitOne();
        needRerun  = IsRunning;
        ServiceMutex.ReleaseMutex();
        if (needRerun) StartReceive();
    }
}
