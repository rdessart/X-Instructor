using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using XInstructor.Common.Messages;
using XInstructor.Common.Models;
using XInstructor.Common.Services;

namespace XInstructor.Common.ViewModels;

public partial class HomeViewModel : BaseViewModel, IRecipient<BeaconReceiveMessage>
{
    private readonly ClientManagerService _clientManagerService;
    private readonly BeaconLocatorService _beaconLocatorService;

    [ObservableProperty]
    private bool _beaconLocatorRunning = false;

    [ObservableProperty]
    private int  _beaconPort = 50888;

    [ObservableProperty]
    private string _beaconStartText = "START";

    [RelayCommand]
    private void SwitchService()
    {
        if(!BeaconLocatorRunning)
        {
            BeaconLocatorRunning =  _beaconLocatorService.Initalise(BeaconPort);
            _beaconLocatorService.Start();
            BeaconStartText = "STOP";
            return;
        }
        _beaconLocatorService.Stop();
        BeaconLocatorRunning = false;
        BeaconStartText = "START";
    }

    [ObservableProperty]
    private ObservableCollection<ClientModel> _beacons = [];

    [ObservableProperty]
    private ClientModel? _selectedBeacon = null;

    partial void OnSelectedBeaconChanged(ClientModel? value)
    {
        if (value != null)
        {
            _clientManagerService.AddClient(value);
        }
    }


    //[RelayCommand]
    //private void SelectionChanged()
    //{

    //}


    [RelayCommand]
    private void ClearSelection()
    ///<summary>Clear selection and refresh the UI state</summary>
    {
        SelectedBeacon = null;
        var beacons = Beacons;
        Beacons = beacons;
    }

    public HomeViewModel(BeaconLocatorService beaconLocatorService, ClientManagerService clientManagerService) 
    {
        _beaconLocatorService = beaconLocatorService;
        _clientManagerService = clientManagerService;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(BeaconReceiveMessage message)
    {
        var selClient = Beacons.FirstOrDefault(
            b => /*b.RemoteAddress.Address == message.Value.RemoteEP.Address.Address*/
                 b.RemoteAddress.Equals(message.Value.RemoteEP.Address)  &&
                 b.RemotePort == message.Value.RemoteEP.Port);
        if (selClient != null)
        {
            selClient.AircraftAuthor = message.Value.AircraftAuthor;
            selClient.AircraftDescription = message.Value.AircraftDescription;
            selClient.LastBeacon = message.Value.BeaconTimeStampZulu.ToLocalTime();
        }
        else
        {
            var client = new ClientModel();
            client.FromBeacon(message.Value);
            Beacons.Add(client);
        }
    }
}
