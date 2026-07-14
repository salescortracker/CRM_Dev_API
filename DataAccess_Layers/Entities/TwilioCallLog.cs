using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TwilioCallLog
{
    public Guid TwilioCallLogId { get; set; }

    public Guid? LeadCallId { get; set; }

    public Guid TwilioConfigurationId { get; set; }

    public string CallSid { get; set; } = null!;

    public string? ParentCallSid { get; set; }

    public string FromNumber { get; set; } = null!;

    public string ToNumber { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? DurationSeconds { get; set; }

    public decimal? Price { get; set; }

    public string? PriceUnit { get; set; }

    public DateTime? StartedOn { get; set; }

    public DateTime? EndedOn { get; set; }

    public string? RecordingSid { get; set; }

    public string? RecordingUrl { get; set; }

    public string? RawResponseJson { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual ICollection<CallRecording> CallRecordings { get; set; } = new List<CallRecording>();

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual LeadCall? LeadCall { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual TwilioConfiguration TwilioConfiguration { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
