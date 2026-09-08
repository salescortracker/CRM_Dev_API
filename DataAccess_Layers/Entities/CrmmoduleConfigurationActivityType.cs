using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationActivityType
{
    public int ActivityTypeId { get; set; }

    public string ActivityName { get; set; } = null!;

    public string ActivityCode { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public string? Description { get; set; }

    public int ReminderBeforeMinutes { get; set; }

    public string Status { get; set; } = null!;

    public bool ReminderRequired { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
