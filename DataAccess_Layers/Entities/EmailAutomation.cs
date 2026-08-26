using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EmailAutomation
{
    public int EmailAutomationId { get; set; }

    public string AutomationName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string TriggerEvent { get; set; } = null!;

    public int? EmailTemplateId { get; set; }

    public string RecipientType { get; set; } = null!;

    public string ScheduleType { get; set; } = null!;

    public int? DelayMinutes { get; set; }

    public string? FromEmail { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<EmailAutomationRecipient> EmailAutomationRecipients { get; set; } = new List<EmailAutomationRecipient>();
}
