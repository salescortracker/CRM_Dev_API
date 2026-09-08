using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadSetting
{
    public int LeadSettingId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string SettingName { get; set; } = null!;

    public string LeadStatus { get; set; } = null!;

    public string LeadPriority { get; set; } = null!;

    public string AssignmentRule { get; set; } = null!;

    public int FollowUpDays { get; set; }

    public string Status { get; set; } = null!;

    public bool EnableAutoAssignment { get; set; }

    public bool EmailNotification { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
