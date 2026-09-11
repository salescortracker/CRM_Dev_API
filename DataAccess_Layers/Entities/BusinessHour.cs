using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class BusinessHour
{
    public int BusinessHoursId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string BusinessHoursName { get; set; } = null!;

    public int BranchId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public TimeOnly? BreakStart { get; set; }

    public TimeOnly? BreakEnd { get; set; }

    public string WorkingDays { get; set; } = null!;

    public string? Weekend { get; set; }

    public decimal? TotalWorkingHours { get; set; }

    public int? LateMarkGraceTimeMinutes { get; set; }

    public decimal? HalfDayThresholdHours { get; set; }

    public bool FlexibleHours { get; set; }

    public bool OvertimeAllowed { get; set; }

    public string? Description { get; set; }

    public bool Active { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Branch1 Branch { get; set; } = null!;
}
