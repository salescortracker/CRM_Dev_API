using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ScheduledJob
{
    public int ScheduledJobId { get; set; }

    public string JobName { get; set; } = null!;

    public string JobType { get; set; } = null!;

    public string? Description { get; set; }

    public string Frequency { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public int? RepeatEvery { get; set; }

    public int? DayOfWeek { get; set; }

    public int? DayOfMonth { get; set; }

    public string ActionType { get; set; } = null!;

    public string? Parameters { get; set; }

    public int? RetryCount { get; set; }

    public int? TimeoutMinutes { get; set; }

    public DateTime? LastRunAt { get; set; }

    public DateTime? NextRunAt { get; set; }

    public string? LastRunStatus { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
