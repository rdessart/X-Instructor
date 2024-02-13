namespace XInstructor.Common.Models.Network;

public class AIOperation : NetworkOperation
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Elevation { get; set; }

    public double Vx { get; set; }

    public double Vy { get; set; }

    public double Vz { get; set; }

    public double Pitch { get; set; }

    public double Roll { get; set; }

    public double Heading { get; set; }

    public int AircraftId { get; set; } = 1;

    public AIOperation() : base("UpdatePlane")
    {
    }
}
