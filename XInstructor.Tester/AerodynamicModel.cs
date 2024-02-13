using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XInstructor.Tester;

class AerodynamicModel
{
    // Constants for the lift and drag coefficient equations
    const double LiftAoACoefficient = 0.1;
    const double DragAoACoefficient = 0.05;
    const double LiftMachCoefficient = 0.01;
    const double DragMachCoefficient = 0.02;

    // Function to calculate lift coefficient based on angle of attack
    static double CalculateLiftCoefficient(double angleOfAttack)
    {
        // Example polynomial function for lift coefficient vs. angle of attack
        return LiftAoACoefficient * angleOfAttack + LiftMachCoefficient * Math.Pow(MachNumber(), 2);
    }

    // Function to calculate drag coefficient based on angle of attack
    static double CalculateDragCoefficient(double angleOfAttack)
    {
        // Example polynomial function for drag coefficient vs. angle of attack
        return DragAoACoefficient * angleOfAttack + DragMachCoefficient * Math.Pow(MachNumber(), 2);
    }

    // Function to calculate Mach number (for simplicity, you may want to replace this with a more accurate model)
    static double MachNumber()
    {
        // Example: Mach number is assumed to be constant for simplicity
        return 0.8; // Mach number of 0.8
    }

    static double CalculateLift(double speed, double aoa)
    {
        double cl = CalculateLiftCoefficient(aoa);
        return cl * Math.Pow(speed, 2);
    }

    // Function to calculate drag based on speed
    static double CalculateDrag(double speed, double aoa)
    {
        double cd = CalculateDragCoefficient(aoa);
        return cd * Math.Pow(speed, 2);
    }

    //static void Main()
    //{
    //    Console.WriteLine("Boeing 737-800 Aerodynamics Calculator");
    //    Console.WriteLine("---------------------------------------");

    //    Console.Write("Enter the speed of the Boeing 737-800 (in m/s): ");
    //    double speed = double.Parse(Console.ReadLine());

    //    double lift = CalculateLift(speed);
    //    double drag = CalculateDrag(speed);

    //    Console.WriteLine($"At a speed of {speed} m/s:");
    //    Console.WriteLine($"Lift: {lift} N");
    //    Console.WriteLine($"Drag: {drag} N");
    //}
    //static void Main()
    //{
    //    Console.WriteLine("Boeing 737-800 Aerodynamics Model");
    //    Console.WriteLine("---------------------------------");

    //    Console.Write("Enter the angle of attack (in degrees): ");
    //    double angleOfAttack = double.Parse(Console.ReadLine());

    //    double liftCoefficient = CalculateLiftCoefficient(angleOfAttack);
    //    double dragCoefficient = CalculateDragCoefficient(angleOfAttack);

    //    Console.WriteLine($"At an angle of attack of {angleOfAttack} degrees and Mach number of {MachNumber()}:");
    //    Console.WriteLine($"Lift Coefficient: {liftCoefficient}");
    //    Console.WriteLine($"Drag Coefficient: {dragCoefficient}");
    //}
}
