using CommunityToolkit.Mvvm.Messaging;
using XInstructor.Common.Messages;
using XInstructor.Common.Services;

Console.WriteLine("X-INSTRUCTOR CMD TEST...");

BeaconLocatorService beaconLocator = new BeaconLocatorService();
if(!beaconLocator.Initalise(50888))
{
    Console.WriteLine($"Unable to initalise beacon locator");
    return;
}
//beaconLocator.BeaconReceived += (_, beacon) =>
//{
//    Console.WriteLine($"Beacon from {beacon.RemoteEP} at {beacon.BeaconTimeStampZulu.ToLocalTime()} LT");
//};

//WeakReferenceMessenger.Default.Register<BeaconReceiveMessage>(this, (r, beacon) =>
//{
//    Console.WriteLine($"Beacon from {beacon.Value.RemoteEP} at {beacon.Value.BeaconTimeStampZulu.ToLocalTime()} LT");
//});

beaconLocator.Start();

while (true)
{
    Thread.Sleep(100);
}