using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace XInstructor.NavigationDatabase.Entities;

[Keyless]
[Table("tbl_cruising_tables")]
public partial class CruisingTable
{
    [Column("cruise_table_identifier", TypeName = "TEXT(2)")]
    public string? CruiseTableIdentifier { get; set; }

    [Column("seqno", TypeName = "INTEGER(3)")]
    public int? Seqno { get; set; }

    [Column("course_from", TypeName = "DOUBLE(5)")]
    public double? CourseFrom { get; set; }

    [Column("course_to", TypeName = "DOUBLE(5)")]
    public double? CourseTo { get; set; }

    [Column("mag_true", TypeName = "TEXT(1)")]
    public string? MagTrue { get; set; }

    [Column("cruise_level_from1", TypeName = "INTEGER(5)")]
    public int? CruiseLevelFrom1 { get; set; }

    [Column("vertical_separation1", TypeName = "INTEGER(5)")]
    public int? VerticalSeparation1 { get; set; }

    [Column("cruise_level_to1", TypeName = "INTEGER(5)")]
    public int? CruiseLevelTo1 { get; set; }

    [Column("cruise_level_from2", TypeName = "INTEGER(5)")]
    public int? CruiseLevelFrom2 { get; set; }

    [Column("vertical_separation2", TypeName = "INTEGER(5)")]
    public int? VerticalSeparation2 { get; set; }

    [Column("cruise_level_to2", TypeName = "INTEGER(5)")]
    public int? CruiseLevelTo2 { get; set; }

    [Column("cruise_level_from3", TypeName = "INTEGER(5)")]
    public int? CruiseLevelFrom3 { get; set; }

    [Column("vertical_separation3", TypeName = "INTEGER(5)")]
    public int? VerticalSeparation3 { get; set; }

    [Column("cruise_level_to3", TypeName = "INTEGER(5)")]
    public int? CruiseLevelTo3 { get; set; }

    [Column("cruise_level_from4", TypeName = "INTEGER(5)")]
    public int? CruiseLevelFrom4 { get; set; }

    [Column("vertical_separation4", TypeName = "INTEGER(5)")]
    public int? VerticalSeparation4 { get; set; }

    [Column("cruise_level_to4", TypeName = "INTEGER(5)")]
    public int? CruiseLevelTo4 { get; set; }
}
