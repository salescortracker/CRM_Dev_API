using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EmailAutomationRecipient
{
    public int EmailAutomationRecipientId { get; set; }

    public int EmailAutomationId { get; set; }

    public string RecipientType { get; set; } = null!;

    public string RecipientValue { get; set; } = null!;

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual EmailAutomation EmailAutomation { get; set; } = null!;
}
