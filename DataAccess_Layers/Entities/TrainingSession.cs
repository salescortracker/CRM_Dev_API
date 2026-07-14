using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TrainingSession
{
    public Guid TrainingSessionId { get; set; }

    public Guid OnboardingProjectId { get; set; }

    public byte TrainingType { get; set; }

    public Guid? TrainerUserId { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public int? AttendeeCount { get; set; }

    public string? MeetingLink { get; set; }

    public string? RecordingUrl { get; set; }

    public string? Feedback { get; set; }

    public byte Status { get; set; }

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

    public virtual OnboardingProject OnboardingProject { get; set; } = null!;

    public virtual User? TrainerUser { get; set; }

    public virtual User User { get; set; } = null!;
}
