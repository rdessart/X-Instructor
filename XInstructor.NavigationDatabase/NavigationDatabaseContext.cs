using Microsoft.EntityFrameworkCore;
using XInstructor.NavigationDatabase.Entities;

namespace XInstructor.NavigationDatabase;

public partial class NavigationDatabaseContext : DbContext
{
    public NavigationDatabaseContext()
    {
    }

    public NavigationDatabaseContext(DbContextOptions<NavigationDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<AirportCommunication> AirportCommunications { get; set; }

    public virtual DbSet<AirportMsa> AirportMsas { get; set; }

    public virtual DbSet<ControlledAirspace> ControlledAirspaces { get; set; }

    public virtual DbSet<CruisingTable> CruisingTables { get; set; }

    public virtual DbSet<EnrouteAirway> EnrouteAirways { get; set; }

    public virtual DbSet<EnrouteAirwayRestriction> EnrouteAirwayRestrictions { get; set; }

    public virtual DbSet<EnrouteCommunication> EnrouteCommunications { get; set; }

    public virtual DbSet<EnrouteNdbnavaid> EnrouteNdbnavaids { get; set; }

    public virtual DbSet<EnrouteWaypoint> EnrouteWaypoints { get; set; }

    public virtual DbSet<FirUir> FirUirs { get; set; }

    public virtual DbSet<Gate> Gates { get; set; }

    public virtual DbSet<Gl> Gls { get; set; }

    public virtual DbSet<GridMora> GridMoras { get; set; }

    public virtual DbSet<Header> Headers { get; set; }

    public virtual DbSet<Holding> Holdings { get; set; }

    public virtual DbSet<Iap> Iaps { get; set; }

    public virtual DbSet<LocalizerMarker> LocalizerMarkers { get; set; }

    public virtual DbSet<LocalizersGlideslope> LocalizersGlideslopes { get; set; }

    public virtual DbSet<Pathpoint> Pathpoints { get; set; }

    public virtual DbSet<RestrictiveAirspace> RestrictiveAirspaces { get; set; }

    public virtual DbSet<Runway> Runways { get; set; }

    public virtual DbSet<Sid> Sids { get; set; }

    public virtual DbSet<Star> Stars { get; set; }

    public virtual DbSet<TerminalNdbnavaid> TerminalNdbnavaids { get; set; }

    public virtual DbSet<TerminalWaypoint> TerminalWaypoints { get; set; }

    public virtual DbSet<Vhfnavaid> Vhfnavaids { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "X-Instructor", "database.db");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
