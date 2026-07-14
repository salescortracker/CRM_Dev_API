using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TwilioSmslog
{
    public Guid TwilioSmslogId { get; set; }

    public Guid TwilioConfigurationId { get; set; }

    public Guid? LeadId { get; set; }

    public Guid? CustomerId { get; set; }

    public string MessageSid { get; set; } = null!;

    public string FromNumber { get; set; } = null!;

    public string ToNumber { get; set; } = null!;

    public string MessageBody { get; set; } = null!;

    public byte Direction { get; set; }

    public string Status { get; set; } = null!;

    public decimal? Price { get; set; }

    public DateTime? SentOn { get; set; }

    public DateTime? DeliveredOn { get; set; }

    public string? FailureReason { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Lead? Lead { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual TwilioConfiguration TwilioConfiguration { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
