using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CallRecording
{
    public Guid CallRecordingId { get; set; }

    public Guid? LeadCallId { get; set; }

    public Guid? TwilioCallLogId { get; set; }

    public string RecordingSid { get; set; } = null!;

    public string RecordingUrl { get; set; } = null!;

    public int RecordingDurationSeconds { get; set; }

    public long? FileSizeBytes { get; set; }

    public byte RecordingStatus { get; set; }

    public bool ConsentCaptured { get; set; }

    public DateOnly? RetentionEndDate { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual LeadCall? LeadCall { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual TwilioCallLog? TwilioCallLog { get; set; }

    public virtual User User { get; set; } = null!;
}
