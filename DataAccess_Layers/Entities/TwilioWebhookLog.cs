using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TwilioWebhookLog
{
    public Guid TwilioWebhookLogId { get; set; }

    public Guid TwilioConfigurationId { get; set; }

    public string WebhookType { get; set; } = null!;

    public string? RequestUrl { get; set; }

    public string? RequestMethod { get; set; }

    public string RequestPayload { get; set; } = null!;

    public string? ResponsePayload { get; set; }

    public byte ProcessingStatus { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime ReceivedOn { get; set; }

    public DateTime? ProcessedOn { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual TwilioConfiguration TwilioConfiguration { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
