using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadCall
{
    public Guid LeadCallId { get; set; }

    public Guid LeadId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid SalesUserId { get; set; }

    public Guid? TwilioConfigurationId { get; set; }

    public string? TwilioCallSid { get; set; }

    public string FromNumber { get; set; } = null!;

    public string ToNumber { get; set; } = null!;

    public byte CallDirection { get; set; }

    public string CallStatus { get; set; } = null!;

    public DateTime? CallStartedOn { get; set; }

    public DateTime? CallEndedOn { get; set; }

    public int? CallDurationSeconds { get; set; }

    public string? CallOutcome { get; set; }

    public string? RecordingUrl { get; set; }

    public int? RecordingDurationSeconds { get; set; }

    public string? Notes { get; set; }

    public DateTime? NextFollowUpOn { get; set; }

    public string? FailureReason { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<CallRecording> CallRecordings { get; set; } = new List<CallRecording>();

    public virtual Lead Lead { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual User SalesUser { get; set; } = null!;

    public virtual ICollection<TwilioCallLog> TwilioCallLogs { get; set; } = new List<TwilioCallLog>();
}
