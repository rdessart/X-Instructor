using GeographicLib;
using System.Globalization;
using System.Net;
using System.Text.Json;
using UnitsNet;
using UnitsNet.Units;
using XInstructor.Common.Models.Network;
using XInstructor.Common.Services;
using XInstructor.NavigationDatabase;
using XInstructor.NavigationDatabase.Entities;
using XInstructor.Tester;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


NavigationDatabaseContext navigationDatabase = new NavigationDatabaseContext();
UDPSimulatorService simulatorService = new UDPSimulatorService();
simulatorService.Initalize();

var runway = navigationDatabase.Runways.First(r => r.AirportIdentifier == "EBOS" && r.RunwayIdentifier == "RW08");
double track = runway.RunwayTrueBearing ?? -1.0;
double elevation_offset_plane = 5.56;
AIOperation position = new AIOperation()
{
    Pitch = 0.0,
    Roll = 0.0,
    Heading = track,
    Elevation = new Length(runway.LandingThresholdElevation ?? 0.0, LengthUnit.Foot).Meters + elevation_offset_plane,
    Latitude = runway.RunwayLatitude ?? 0.0,
    Longitude = runway.RunwayLongitude ?? 0.0,
    Vx = 0.0,
    Vy = 0.0,
    Vz = 0.0,
    AircraftId = 1,
};


if(runway.DisplacedThresholdDistance > 0)
{
    double displacedTrehsold = new Length(runway.DisplacedThresholdDistance ?? 0.0, LengthUnit.Foot).Meters;
    var startRunway = Geodesic.WGS84.Direct(position.Latitude, position.Longitude, (track + 180.0) % 360.0, displacedTrehsold, GeodesicFlags.Latitude | GeodesicFlags.Longitude);
    position.Latitude = startRunway.Latitude;
    position.Longitude = startRunway.Longitude;
}

double cas = 0.0;
double tas = 0.0;
double max_acceleration = 6.0;

double target_altitude = new Length(3000, LengthUnit.Foot).Meters;
double rotation_velocity = new Speed(140.0, SpeedUnit.Knot).MetersPerSecond;
double target_velocity = new Speed(160.0, SpeedUnit.Knot).MetersPerSecond;
double ax = 0.0;
double ay = 0.0;
double vy = 0.0;
double distance = 0.0;
double deltaTime = 0.01;


Console.WriteLine("Ready press ENTER to send data...");
Console.ReadKey();

while (position.Elevation < target_altitude)
{
    double acceleration = max_acceleration - (position.Elevation / 100 * 2.0);
    double prevDistance = distance;

    double prevEle = position.Elevation;
    double pitch_rad = new Angle(position.Pitch, AngleUnit.Degree).Radians;
    tas = cas / (1 + (position.Elevation * 0.00002));

    ax = Math.Cos(pitch_rad) * acceleration;
    ay = Math.Sin(pitch_rad) * acceleration;

    if(cas < target_velocity)
        cas += (ax * deltaTime);

    if(cas > rotation_velocity)
    {
        if (position.Pitch <= 15)
            position.Pitch += 3.0 * deltaTime;
        vy += ay * deltaTime;
        position.Elevation += vy * deltaTime;
    }
    distance += cas * deltaTime;
    double deltaDistance = distance - prevDistance;

    double slope = (position.Elevation - prevEle) / deltaDistance;
    double slopeAngle = new Angle(Math.Atan(slope), AngleUnit.Radian).Degrees;

    double AOA = position.Pitch - slopeAngle;

    var result = Geodesic.WGS84.Direct(position.Latitude, position.Longitude, track, deltaDistance, GeodesicFlags.Latitude | GeodesicFlags.Longitude);
    position.Latitude = result.Latitude;
    position.Longitude = result.Longitude;

    Console.WriteLine($"[{position.Latitude:00.0000}°N {position.Longitude:000.0000}°E] - {new Length(position.Elevation, LengthUnit.Meter).Feet:0000.0} ft - " +
        $"{new Speed(cas, SpeedUnit.MeterPerSecond).Knots:000.0} kts - {new Speed(tas, SpeedUnit.MeterPerSecond).Knots:000.0} kts - {position.Pitch:00.0}° - AOA {AOA:00.0}° - {acceleration:0.00} m/s²");

    string text = JsonSerializer.Serialize<NetworkOperation>(position);

    simulatorService.SendData(text, new IPEndPoint(IPAddress.Loopback, 50555));

    Thread.Sleep((int)(deltaTime * 1000.0));
}
