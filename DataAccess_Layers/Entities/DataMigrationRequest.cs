using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class DataMigrationRequest
{
    public Guid DataMigrationRequestId { get; set; }

    public Guid OnboardingProjectId { get; set; }

    public byte MigrationType { get; set; }

    public string? SourceSystem { get; set; }

    public string? FileUrl { get; set; }

    public int? RecordCount { get; set; }

    public int? MigratedCount { get; set; }

    public int? FailedCount { get; set; }

    public byte Status { get; set; }

    public DateTime RequestedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public string? ErrorLog { get; set; }

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

    public virtual User User { get; set; } = null!;
}
