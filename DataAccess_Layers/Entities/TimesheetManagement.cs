using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TimesheetManagement
{
    public int TimesheetId { get; set; }

    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public int? TaskId { get; set; }

    public DateOnly TimesheetDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public decimal TotalHours { get; set; }

    public string BillingType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public string? WorkDescription { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProjectManagement Project { get; set; } = null!;

    public virtual ProjectTask? Task { get; set; }
}
