using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Slarule
{
    public int SlaruleId { get; set; }

    public string Slaname { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string Priority { get; set; } = null!;

    public int FirstResponseMinutes { get; set; }

    public int ResolutionMinutes { get; set; }

    public int? WarningMinutes { get; set; }

    public int? BusinessHoursId { get; set; }

    public int? HolidayCalendarId { get; set; }

    public bool EscalationEnabled { get; set; }

    public int? EscalationRuleId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual EscalationRule? EscalationRule { get; set; }
}
