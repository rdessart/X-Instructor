﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace XInstructor.NavigationDatabase.Entities;

[Keyless]
[Table("tbl_airports")]
public partial class Airport
{
    [Column("area_code", TypeName = "TEXT(3)")]
    public string? AreaCode { get; set; }

    [Column("icao_code", TypeName = "TEXT(2)")]
    public string IcaoCode { get; set; } = null!;

    [Column("airport_identifier", TypeName = "TEXT(4)")]
    public string AirportIdentifier { get; set; } = null!;

    [Column("airport_identifier_3letter", TypeName = "TEXT(3)")]
    public string? AirportIdentifier3letter { get; set; }

    [Column("airport_name", TypeName = "TEXT(3)")]
    public string? AirportName { get; set; }

    [Column("airport_ref_latitude", TypeName = "DOUBLE(9)")]
    public double? AirportRefLatitude { get; set; }

    [Column("airport_ref_longitude", TypeName = "DOUBLE(10)")]
    public double? AirportRefLongitude { get; set; }

    [Column("ifr_capability", TypeName = "TEXT(1)")]
    public string? IfrCapability { get; set; }

    [Column("longest_runway_surface_code", TypeName = "TEXT(1)")]
    public string? LongestRunwaySurfaceCode { get; set; }

    [Column("elevation", TypeName = "INTEGER(5)")]
    public int? Elevation { get; set; }

    [Column("transition_altitude", TypeName = "INTEGER(5)")]
    public int? TransitionAltitude { get; set; }

    [Column("transition_level", TypeName = "INTEGER(5)")]
    public int? TransitionLevel { get; set; }

    [Column("speed_limit", TypeName = "INTEGER(3)")]
    public int? SpeedLimit { get; set; }

    [Column("speed_limit_altitude", TypeName = "INTEGER(5)")]
    public int? SpeedLimitAltitude { get; set; }

    [Column("iata_ata_designator", TypeName = "TEXT(3)")]
    public string? IataAtaDesignator { get; set; }

    [Column("id", TypeName = "TEXT(15)")]
    public string? Id { get; set; }
}
