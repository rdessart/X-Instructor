namespace XInstructor.Tester;

public class Atmosphere
{
    static double SpeedOfSound(double altitude)
    {
        // Simplified model for speed of sound at altitude (in m/s)
        const double SeaLevelSpeedOfSound = 340.29; // Speed of sound at sea level in m/s
        const double TemperatureLapseRate = 0.0065; // Standard temperature lapse rate in K/m
        const double SeaLevelTemperature = 288.15; // Standard sea level temperature in K

        // Calculate temperature at given altitude
        double temperature = SeaLevelTemperature - TemperatureLapseRate * altitude;

        // Calculate speed of sound using ideal gas law
        return Math.Sqrt(1.4 * 287.05 * temperature);
    }

    // Function to calculate true airspeed from Mach number and altitude
    static double TrueAirspeed(double machNumber, double altitude)
    {
        // Calculate speed of sound at given altitude
        double speedOfSound = SpeedOfSound(altitude);

        // Calculate true airspeed using the relationship between Mach number and true airspeed
        return machNumber * speedOfSound;
    }

}
