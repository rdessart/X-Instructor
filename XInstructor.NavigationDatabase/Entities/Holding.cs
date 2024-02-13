﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace XInstructor.NavigationDatabase.Entities;

[Keyless]
[Table("tbl_holdings")]
public partial class Holding
{
    [Column("area_code", TypeName = "TEXT(3)")]
    public string? AreaCode { get; set; }

    [Column("region_code", TypeName = "TEXT(4)")]
    public string? RegionCode { get; set; }

    [Column("icao_code", TypeName = "TEXT(2)")]
    public string? IcaoCode { get; set; }

    [Column("waypoint_identifier", TypeName = "TEXT(5)")]
    public string? WaypointIdentifier { get; set; }

    [Column("holding_name", TypeName = "TEXT(25)")]
    public string? HoldingName { get; set; }

    [Column("waypoint_latitude", TypeName = "DOUBLE(9)")]
    public double? WaypointLatitude { get; set; }

    [Column("waypoint_longitude", TypeName = "DOUBLE(10)")]
    public double? WaypointLongitude { get; set; }

    [Column("duplicate_identifier", TypeName = "INTEGER(2)")]
    public int? DuplicateIdentifier { get; set; }

    [Column("inbound_holding_course", TypeName = "DOUBLE(5)")]
    public double? InboundHoldingCourse { get; set; }

    [Column("turn_direction", TypeName = "TEXT(1)")]
    public string? TurnDirection { get; set; }

    [Column("leg_length", TypeName = "DOUBLE(4)")]
    public double? LegLength { get; set; }

    [Column("leg_time", TypeName = "DOUBLE(3)")]
    public double? LegTime { get; set; }

    [Column("minimum_altitude", TypeName = "INTEGER(5)")]
    public int? MinimumAltitude { get; set; }

    [Column("maximum_altitude", TypeName = "INTEGER(5)")]
    public int? MaximumAltitude { get; set; }

    [Column("holding_speed", TypeName = "INTEGER(3)")]
    public int? HoldingSpeed { get; set; }
}
