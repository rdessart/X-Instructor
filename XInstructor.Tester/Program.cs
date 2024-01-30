using XInstructor.Common.Services;

Console.WriteLine("X-INSTRUCTOR CMD TEST...");

BeaconLocatorService beaconLocator = new BeaconLocatorService();
if(!beaconLocator.Initalise(50888))
{
    Console.WriteLine($"Unable to initalise beacon locator");
    return;
}
beaconLocator.Start();
while (true)
{
    Thread.Sleep(100);
}