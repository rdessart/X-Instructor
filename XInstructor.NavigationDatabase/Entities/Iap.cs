﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace XInstructor.NavigationDatabase.Entities;

[Keyless]
[Table("tbl_iaps")]
public partial class Iap
{
    [Column("area_code", TypeName = "TEXT(3)")]
    public string? AreaCode { get; set; }

    [Column("airport_identifier", TypeName = "TEXT(4)")]
    public string? AirportIdentifier { get; set; }

    [Column("procedure_identifier", TypeName = "TEXT(6)")]
    public string? ProcedureIdentifier { get; set; }

    [Column("route_type", TypeName = "TEXT(1)")]
    public string? RouteType { get; set; }

    [Column("transition_identifier", TypeName = "TEXT(5)")]
    public string? TransitionIdentifier { get; set; }

    [Column("seqno", TypeName = "INTEGER(3)")]
    public int? Seqno { get; set; }

    [Column("waypoint_icao_code", TypeName = "TEXT(2)")]
    public string? WaypointIcaoCode { get; set; }

    [Column("waypoint_identifier", TypeName = "TEXT(5)")]
    public string? WaypointIdentifier { get; set; }

    [Column("waypoint_latitude", TypeName = "DOUBLE(9)")]
    public double? WaypointLatitude { get; set; }

    [Column("waypoint_longitude", TypeName = "DOUBLE(10)")]
    public double? WaypointLongitude { get; set; }

    [Column("waypoint_description_code", TypeName = "TEXT(4)")]
    public string? WaypointDescriptionCode { get; set; }

    [Column("turn_direction", TypeName = "TEXT(1)")]
    public string? TurnDirection { get; set; }

    [Column("rnp", TypeName = "DOUBLE(4)")]
    public double? Rnp { get; set; }

    [Column("path_termination", TypeName = "TEXT(2)")]
    public string? PathTermination { get; set; }

    [Column("recommanded_navaid", TypeName = "TEXT(4)")]
    public string? RecommandedNavaid { get; set; }

    [Column("recommanded_navaid_latitude", TypeName = "DOUBLE(9)")]
    public double? RecommandedNavaidLatitude { get; set; }

    [Column("recommanded_navaid_longitude", TypeName = "DOUBLE(10)")]
    public double? RecommandedNavaidLongitude { get; set; }

    [Column("arc_radius", TypeName = "DOUBLE(7)")]
    public double? ArcRadius { get; set; }

    [Column("theta", TypeName = "DOUBLE(5)")]
    public double? Theta { get; set; }

    [Column("rho", TypeName = "DOUBLE(5)")]
    public double? Rho { get; set; }

    [Column("magnetic_course", TypeName = "DOUBLE(5)")]
    public double? MagneticCourse { get; set; }

    [Column("route_distance_holding_distance_time", TypeName = "DOUBLE(5)")]
    public double? RouteDistanceHoldingDistanceTime { get; set; }

    [Column("distance_time", TypeName = "TEXT(1)")]
    public string? DistanceTime { get; set; }

    [Column("altitude_description", TypeName = "TEXT(1)")]
    public string? AltitudeDescription { get; set; }

    [Column("altitude1", TypeName = "INTEGER(5)")]
    public int? Altitude1 { get; set; }

    [Column("altitude2", TypeName = "INTEGER(5)")]
    public int? Altitude2 { get; set; }

    [Column("transition_altitude", TypeName = "INTEGER(5)")]
    public int? TransitionAltitude { get; set; }

    [Column("speed_limit_description", TypeName = "TEXT(1)")]
    public string? SpeedLimitDescription { get; set; }

    [Column("speed_limit", TypeName = "INTEGER(3)")]
    public int? SpeedLimit { get; set; }

    [Column("vertical_angle", TypeName = "DOUBLE(4)")]
    public double? VerticalAngle { get; set; }

    [Column("center_waypoint", TypeName = "TEXT(5)")]
    public string? CenterWaypoint { get; set; }

    [Column("center_waypoint_latitude", TypeName = "DOUBLE(9)")]
    public double? CenterWaypointLatitude { get; set; }

    [Column("center_waypoint_longitude", TypeName = "DOUBLE(10)")]
    public double? CenterWaypointLongitude { get; set; }

    [Column("aircraft_category", TypeName = "TEXT(1)")]
    public string? AircraftCategory { get; set; }

    [Column("id", TypeName = "TEXT(15)")]
    public string? Id { get; set; }

    [Column("recommanded_id", TypeName = "TEXT(15)")]
    public string? RecommandedId { get; set; }

    [Column("center_id", TypeName = "TEXT(15)")]
    public string? CenterId { get; set; }
}
