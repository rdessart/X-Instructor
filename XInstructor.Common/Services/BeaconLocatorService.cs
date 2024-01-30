using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

    //public event 

    public BeaconLocatorService()
    {
        _remoteEP = new IPEndPoint(IPAddress.Any, 0);
        _socket = new Socket(socketType: SocketType.Dgram, protocolType: ProtocolType.Udp, addressFamily: AddressFamily.InterNetwork);
        _bindEndPoint = new IPEndPoint(IPAddress.Any, 0);
        _buffer = new byte[500];
    }

    public bool Initalise(int targetPort = 50888)
    {
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
        return true;
    }

    public void Start() 
    {
        StartReceive();
    }

    private void StartReceive()
    {
        _socket.BeginReceiveFrom(_buffer, 0, 500, SocketFlags.None, ref _remoteEP, EndReceive, _socket);
    }

    private void EndReceive(IAsyncResult ar)
    {
        int received = (ar.AsyncState as Socket)!.EndReceiveFrom(ar, ref _remoteEP);
        string data = Encoding.ASCII.GetString(_buffer, 0, received);
        Debug.WriteLine($"Received {data}");
        StartReceive();
    }
}
